# Proposal

## Why

The in-house mapper only copies same-name flat members by reflection. Future profiles will need computed or ignored members and nested or collection members, and today each of those would force a hand-written workaround in a handler. A classmate's implementation of the same mapper already solves those cases with compiled delegates, but it silently skips a same-name member whose types do not match, which hides breakage when a model changes. This change takes the capabilities of that design and keeps this project's strict failure at startup.

## What Changes

- Add per-member rules to a profile: `CreateMap<,>()` returns a configuration object with `ForMember(d => d.X, o => o.MapFrom(...))` and `o.Ignore()`.
- Map members whose types differ through a registered map, and map collection members element by element.
- Compile each map into delegates once at startup instead of using reflection on every call.
- Replace the internal `MapperRegistry` with a public `Mapper` built from profiles, so tests construct it directly and `InternalsVisibleTo` is no longer needed.
- Validate at startup that every destination type has a public parameterless constructor.
- Keep failing at startup for a same-name member with incompatible types, now unless an override, a nested map or a collection map converts it.
- Move the mapper types to the `EventsHub.Application.Core.Mapping` namespace to match their folder. **BREAKING** for any `using EventsHub.Application.Core;` that referenced `IMapper`, `Profile`, `MappingProfiles` or `AddMapper`.

## Capabilities

### New Capabilities
<!-- None. -->

### Modified Capabilities
- `object-mapping`: adds per-member overrides, nested and collection members and destination-constructor validation; the "incompatible members" requirement now allows an override or nested map to resolve the mismatch.

## Impact

- **Code:** `src/EventsHub.Application/Core/Mapping/` (all mapper files, `MapperRegistry.cs` removed, new `TypeMap.cs` and `IMappingExpression.cs`), `src/EventsHub.Application/EventsHub.Application.csproj` (drop `InternalsVisibleTo`), `EditEvent.cs` and `src/EventsHub.Api/Program.cs` (`using` only).
- **Tests:** `tests/EventsHub.UnitTests/Core/MapperTests.cs` rewritten and extended; `EditEventTests.cs` and `EventsControllerTests.cs` change their `using`.
- **Docs:** the Mapping section of `src/EventsHub.Application/README.md`.
- **Dependencies:** none added.
- **Out of scope:** merging into an existing nested destination object (a nested member is replaced by a new mapped instance), destination collections other than ones a `List<T>` is assignable to, and projection to `IQueryable`.
