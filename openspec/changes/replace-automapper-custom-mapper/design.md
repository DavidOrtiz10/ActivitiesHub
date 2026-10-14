# Design

## Context

`EventsHub.Application` uses AutoMapper in exactly three places: the `MappingProfiles : Profile` class (`CreateMap<Event, Event>()`), `EditEvent.Handler` (`mapper.Map(request.Event, @event)`), and `services.AddAutoMapper(...)` in `EventsHub.Api/Program.cs`. The map copies a client-supplied `Event` onto the EF-tracked entity, so the mapper must write into an existing instance. `Event` has `required` string members and an init-style GUID `Id`. See proposal.md for motivation.

The Application project already references MediatR and the DI abstractions it needs through the Api host; the OpenApi host does not run handlers, so it needs no mapper registration.

## Goals / Non-Goals

**Goals:**
- Drop-in replacement for the call sites above: `Profile`, `CreateMap<,>()`, `IMapper.Map(source, destination)` keep their shape, so `EditEvent` changes by its `using` only.
- Profiles are plain classes found by assembly scanning, so future profiles are added by creating a class.
- Configuration errors (duplicate map, incompatible member types) surface at startup, not on the first request.

**Non-Goals:**
- Custom member rules (`ForMember`, `MapFrom`, `Ignore`), flattening, collections, and `IQueryable` projection. See Decision 6 for how they would be added.
- Deep copying. Reference-type members are assigned by reference.
- Replacing MediatR or touching any controller, DTO or HTTP contract.

## Decisions

### 1. Live in `EventsHub.Application/Core`, no new project
The mapper is used only by Application handlers, and `MappingProfiles` already lives in `Core`. A new project would add a reference to `EventsHub.slnx`, the OpenApi host and the tests for ~150 lines. *Alternative:* a separate `EventsHub.Mapping` library, rejected as premature until a second consumer exists.

### 2. Public surface mirrors AutoMapper's subset in use
- `IMapper`: `TDestination Map<TDestination>(object source)` and `TDestination Map<TSource, TDestination>(TSource source, TDestination destination)`.
- `Profile`: abstract base with `protected CreateMap<TSource, TDestination>()`.
- `AddMapper(params Assembly[] assemblies)` on `IServiceCollection`.

Keeping the names means `EditEvent` and `Program.cs` change minimally, and the type inference of `mapper.Map(request.Event, @event)` stays valid. *Alternative:* a `Map<TSource, TDestination>(...)` extension-method style with no interface; rejected because handlers take `IMapper` through DI and tests benefit from substituting it.

### 3. Convention is member-name equality, resolved once at startup
When `CreateMap<TSource, TDestination>()` runs, the registry builds a list of member pairs: public readable instance properties on the source and public settable (including `init`) instance properties on the destination with an identical name (ordinal, case-sensitive). A pair whose destination type is not assignable from the source type makes startup throw. The pair list is stored per `(TSource, TDestination)` and reused for every call.

*Alternative considered:* compile getters and setters into expression-tree delegates. Rejected for now: cached `PropertyInfo` access is fast enough for one entity with ten properties and is easier to read for a course project. The pair list is private to the registry, so swapping to compiled delegates later changes no caller.

### 4. Immutable registry shared as a singleton
Profiles are discovered by scanning each given assembly for non-abstract `Profile` subclasses with a public parameterless constructor, instantiating them once and collecting their maps into a read-only dictionary keyed by `(Type, Type)`. The registry and `IMapper` are registered as singletons: the mapper holds no per-request state, so it is thread-safe and handlers can inject it freely.

A duplicate `(TSource, TDestination)` across profiles throws `InvalidOperationException` naming the pair and both profile types.

### 5. Errors
- Null source or null destination → `ArgumentNullException`.
- No map declared for the pair → `InvalidOperationException` whose message names both types. The destination is not touched, because the lookup happens before any member is written.
- `Map<TDestination>(source)` creates the instance with `Activator.CreateInstance` (parameterless constructor, which works with C# `required` members because that rule is enforced at compile time only).

*Alternative:* a dedicated `MappingException`. Rejected; the built-in types carry the same information and there is no caller that needs to catch them specifically.

### 6. Extension point for future profiles
Each map is represented by an internal `TypeMap` holding its member pairs. Convention mapping fills that list; later features (`ForMember`, `Ignore`, `MapFrom`) are additive: `CreateMap` can return a builder that edits the same list before the registry freezes it. No such builder is exposed in this change, so `CreateMap` returns `void`. *If custom member rules are needed in the first future profile, they should be a follow-up change with their own spec requirement.*

## Risks / Trade-offs

- **[Reflection copies by reference]** A mapped reference-type member is shared between source and destination → Acceptable today because `Event` has only value-like members (`string`, `DateTime`, `bool`); document it in the Application README and revisit if a profile maps nested objects.
- **[Convention silently skips unmatched names]** A typo in a destination property name results in an unmapped member rather than an error → Mitigated for the type-mismatch case by the startup check; for unmatched names the spec says "ignored", and the unit test for `Event -> Event` asserts every property is copied.
- **[Behavior drift from AutoMapper]** AutoMapper also maps `Id` and handles null sub-members; the single existing map has neither concern → Covered by an `EditEvent` regression test that edits an event and re-reads it.
- **[Scanning order]** Profile discovery order must not matter → Maps are keyed by type pair and duplicates fail, so order cannot change the result.

## Migration Plan

1. Add the mapper types and unit tests with AutoMapper still referenced.
2. Switch `MappingProfiles`, `EditEvent` and `Program.cs`.
3. Remove the `AutoMapper` package reference and any leftover `using AutoMapper;`.
4. Run `dotnet build EventsHub.slnx` and `dotnet test EventsHub.slnx`; exercise `PUT /api/v1/events/{id}` through the Bruno collection.

Rollback is a plain revert of the change: no database, contract or configuration data is affected.
