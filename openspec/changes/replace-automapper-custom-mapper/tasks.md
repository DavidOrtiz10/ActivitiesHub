# Tasks

## 1. Mapper core

- [ ] 1.1 Add `Profile`, `IMapper`, the internal type-map registry and `Mapper` under `src/EventsHub.Application/Core/`, with the behavior in specs/object-mapping/spec.md; verify `dotnet build src/EventsHub.Application` succeeds with AutoMapper still referenced and no name clash (the new types live in `EventsHub.Application.Core`, AutoMapper's are not imported there yet)
- [ ] 1.2 Add `AddMapper(params Assembly[])` DI extension that scans for `Profile` subclasses and registers the registry and `IMapper` as singletons; verify it with the startup-failure tests in 1.3
- [ ] 1.3 Add `tests/EventsHub.UnitTests/Core/MapperTests.cs` using test-local profiles and types (no database), named `Method_WhenCondition_ExpectedResult` with Arrange/Act/Assert comments; verify `dotnet test tests/EventsHub.UnitTests --filter "FullyQualifiedName~MapperTests"` passes, covering every scenario of the spec:
  - map onto existing instance returns the same instance with all members copied
  - source-only member ignored
  - map to new instance, including a destination with `required` members
  - undeclared pair throws `InvalidOperationException` naming both types and leaves the destination unchanged
  - duplicate map across two profiles fails at startup
  - same-name incompatible member types fail at startup
  - null source throws `ArgumentNullException`

## 2. Switch call sites

- [ ] 2.1 Change `MappingProfiles` to derive from the new `Profile` and keep `CreateMap<Event, Event>()`; remove `using AutoMapper;` from it; verify the file compiles and the profile is found by `AddMapper` (a test resolving `IMapper` from a `ServiceCollection` scanned with the Application assembly can map an `Event` onto another)
- [ ] 2.2 Update `EditEvent.cs` to use the new `IMapper` (drop `using AutoMapper;`, no other change); verify `dotnet build EventsHub.slnx` succeeds
- [ ] 2.3 In `src/EventsHub.Api/Program.cs` replace `AddAutoMapper(typeof(MappingProfiles).Assembly)` with `AddMapper(typeof(MappingProfiles).Assembly)`; verify `dotnet run --project src/EventsHub.Api` starts and applies migrations without an error log
- [ ] 2.4 Add a unit test that runs `EditEvent.Handler` through a mapper resolved from DI against `GlobalTestSetup.AppDbContext`, edits a seeded event and re-reads it, restoring the original values in `[TearDown]` because the test database is shared; verify the test passes and the other `EventsControllerTests` still pass

## 3. Remove AutoMapper and document

- [ ] 3.1 Remove the `AutoMapper` `PackageReference` from `src/EventsHub.Application/EventsHub.Application.csproj`; verify `Select-String -Path src,tests -Pattern "AutoMapper" -Recurse -Include *.cs,*.csproj` returns nothing and `dotnet build EventsHub.slnx` succeeds
- [ ] 3.2 Add a "Mapping" section to `src/EventsHub.Application/README.md` covering: where the mapper lives, how `Event -> Event` is declared, how to add a profile, and the limits (convention by name only, by-reference copy); verify the documented snippet matches the real `MappingProfiles.cs`
- [ ] 3.3 Run `dotnet test EventsHub.slnx` and, with the API running, the Bruno request `Events - Edit - 204`; verify all unit tests pass and the edit request returns `204` (it needs an event id that exists in the local database)
- [ ] 3.4 Run `graphify update .` so the knowledge graph reflects the new mapper types; verify `graphify query "How MappingProfiles connects with IMapper"` shows the new `Profile` and `Mapper` nodes

## Workflow follow-up

- Archive the change after review with `/opsx:archive`; this creates `openspec/specs/object-mapping/spec.md`.
