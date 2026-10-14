# Proposal

## Why

`EventsHub.Application` depends on AutoMapper for a single job: copying an incoming `Event` onto the tracked entity in `EditEvent.Handler`. The only profile is `CreateMap<Event, Event>()`. The project is a course exercise whose goal is to understand and own its application layer, and a third-party mapper hides the mechanism behind reflection and conventions. A small in-house mapper keeps the same `Profile` style but removes the dependency and lets future profiles be shaped by this project's needs.

## What Changes

- Add a custom mapping component in `EventsHub.Application/Core`: an `IMapper` abstraction, a `Profile` base class with `CreateMap<TSource, TDestination>()`, and a registry that discovers profiles by assembly scanning.
- Support mapping onto an existing destination instance (`Map(source, destination)`) and to a new instance (`Map<TDestination>(source)`), matching by member name.
- Rewrite `MappingProfiles` to derive from the new `Profile` and keep the `Event -> Event` map, so `EditEvent` behaves as it does today.
- Add `AddMapper(assembly)` DI registration and call it from `EventsHub.Api/Program.cs` in place of `AddAutoMapper`.
- Remove the `AutoMapper` NuGet package from `EventsHub.Application.csproj`.
- Add unit tests for the mapper (the test project currently has none for Application code).
- Document the mapper and how to add a profile in the `EventsHub.Application` README.

No HTTP contract, database schema or endpoint behavior changes.

## Capabilities

### New Capabilities
- `object-mapping`: how the application layer maps one object type to another through named profiles, including profile discovery, mapping onto an existing instance, and failure when a map is missing.

### Modified Capabilities
<!-- None: openspec/specs/ is empty, so there is no existing capability to modify. -->

## Impact

- **Code:** `src/EventsHub.Application/Core/` (new mapper types, `MappingProfiles.cs`), `src/EventsHub.Application/Events/Commands/EditEvent.cs` (`using` and injected type), `src/EventsHub.Application/EventsHub.Application.csproj`, `src/EventsHub.Api/Program.cs`.
- **Tests:** new fixture under `tests/EventsHub.UnitTests/`.
- **Dependencies:** removes `AutoMapper` 13.0.1. No new packages.
- **Docs:** `src/EventsHub.Application/README.md` gains a section on the mapper and how to add a profile. No existing doc under the repo's README, `Docs/Architecture.md` or `CLAUDE.md` mentions AutoMapper, so nothing needs removing. `Docs/Fundamentals/CleanArchitectureFundamentals.md` is a generic guide and is left as is.
- **Out of scope:** DTOs, new profiles beyond `Event -> Event`, mapping collections or projection to `IQueryable`.
