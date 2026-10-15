# Tasks

## 1. Mapper core

- [x] 1.1 In `src/EventsHub.Application/Core/Mapping/`, add `IMappingExpression.cs` (`IMappingExpression`, `IMemberOptions`) and `TypeMap.cs` (abstract `TypeMap` plus `TypeMap<TSource, TDestination>` with compiled copy and factory delegates, `ForMember`, nested and collection conversion, strict mismatch error, destination-constructor check); verify `dotnet build src/EventsHub.Application` succeeds
- [x] 1.2 Rewrite `Profile.cs` (`CreateMap` returns `IMappingExpression`), `IMapper.cs` and `Mapper.cs` (public, built from profiles, duplicate detection, missing-map and null errors), and `MapperServiceCollectionExtensions.cs` (`AddMapper(params Assembly[])` that unwraps profile-constructor exceptions); delete `MapperRegistry.cs`; move every mapper type and `MappingProfiles` to namespace `EventsHub.Application.Core.Mapping`; verify `dotnet build src/EventsHub.Application` succeeds
- [x] 1.3 Remove `InternalsVisibleTo` from `EventsHub.Application.csproj` and update the `using` lines in `EditEvent.cs`, `src/EventsHub.Api/Program.cs`, `EditEventTests.cs` and `EventsControllerTests.cs`; verify `dotnet build EventsHub.slnx` succeeds
- [x] 1.4 Rewrite `tests/EventsHub.UnitTests/Core/MapperTests.cs` to construct `new Mapper(...)` and cover every scenario of the delta spec (computed, ignored, override without rule, non-writable selector, nested incl. null and any order, collection incl. order, destination without default constructor, same-name incompatible, override resolving it, array destination) besides the existing ones; verify `dotnet test tests/EventsHub.UnitTests --filter "FullyQualifiedName~MapperTests"` passes

## 2. Documentation and end-to-end

- [x] 2.1 Update the Mapping section of `src/EventsHub.Application/README.md` (namespace, `ForMember`, nested and collection rules, new limits); verify every snippet matches the real code
- [x] 2.2 Run `dotnet test EventsHub.slnx` and start the API; verify all tests pass and `PUT /api/v1/events` with unchanged values returns `204`
- [x] 2.3 Run `graphify update .`; verify `graphify query "How Mapper connects with TypeMap"` shows the new `TypeMap` file

## Workflow follow-up

- Archive the change after review with `/opsx:archive`; this merges the delta into `openspec/specs/object-mapping/spec.md`.
