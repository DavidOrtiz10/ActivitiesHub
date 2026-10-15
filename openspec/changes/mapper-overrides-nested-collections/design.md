# Design

## Context

The archived `replace-automapper-custom-mapper` change delivered a convention-only mapper: an internal `MapperRegistry` holding `TypeMap` entries that copy same-name properties with cached `PropertyInfo` reflection, and an internal `Mapper` that depends on it. Tests reach those internals through `InternalsVisibleTo`. A classmate's equivalent (compiled delegates, `ForMember`, nested and collection maps, a public `Mapper`) was reviewed and its 20 unit tests were run; it passes them, but it skips a same-name member with incompatible types without an error and has no test for that case. See proposal.md for motivation.

## Goals / Non-Goals

**Goals:**
- Profiles can compute or ignore a member and map nested and collection members.
- Every configuration mistake still surfaces at startup, not on the first request.
- A change to a model (renamed or retyped property) cannot silently stop a member from being copied.

**Non-Goals:**
- Merging into an existing nested destination object, `IQueryable` projection, or destination collection types other than those a `List<T>` is assignable to.
- Changing how handlers use `IMapper.Map(source, destination)`.

## Decisions

### 1. A public `Mapper` built from profiles replaces `MapperRegistry`
`Mapper` takes the profiles in its constructor, rejects duplicate pairs, then builds every map. The registry only wrapped a dictionary, so it is folded in. Tests construct `new Mapper(profile)` and `InternalsVisibleTo` is removed. *Alternative:* keep the registry and add features to it; rejected because it left two types doing one job.

### 2. Maps are compiled once into delegates
Each `TypeMap<TSource, TDestination>` builds an expression tree of assignments and compiles a copy delegate and a factory at construction. Per-call cost drops from `GetValue`/`SetValue` boxing to a direct delegate call. Maps are built only after all of them are registered, so nested maps resolve regardless of profile order. *Alternative:* keep reflection; rejected because nested and collection members multiply the per-call work and the compile cost is paid once at startup.

### 3. A mismatched member is an error unless something converts it
For each destination property with a source property of the same name, the value expression is, in order: assignable copy, nested map, collection map. If none applies, startup fails naming the map and the member; the fix is a declared map or `ForMember`. This is the one deliberate departure from the classmate's design, which omits the member silently. Destination properties with no same-name source property are still left alone, as the existing spec says.

### 4. Nested members are replaced, not merged
When mapping onto an existing destination, a nested member is set to a new mapped instance (or null if the source is null). Merging would need identity rules for the nested object. *Consequence:* mapping onto a tracked entity with a nested member replaces the navigation object; a profile that needs merging should map the nested member with `ForMember` and `Ignore`.

### 5. Collections target `List<T>`
A collection member maps when the destination type is assignable from `List<TDest>` (`List<T>`, `IList<T>`, `IEnumerable<T>`, `ICollection<T>`, `IReadOnlyList<T>`). Arrays and sets are rejected at startup by Decision 3 instead of being mapped wrongly.

### 6. Namespace follows the folder
Mapper types move to `EventsHub.Application.Core.Mapping`. The folder already changed; keeping the old namespace would leave file location and namespace disagreeing. This touches three `using` lines in `src` and the test files.

### 7. Profiles are discovered with a public parameterless constructor
Unchanged. Exceptions thrown inside a profile constructor are unwrapped from `TargetInvocationException` so the message that reaches the startup log is the real one.

## Risks / Trade-offs

- **[Compiled expressions are harder to debug than reflection]** → Every error path throws before compile with the map and member named, and each new behavior has a unit test.
- **[Cyclic nested data recurses]** A source graph that refers to itself would recurse until stack overflow → Not an issue for `Event`; documented as a limit in the README.
- **[A profile instance belongs to one `Mapper`]** Building two mappers from the same profile instance would rebuild its maps → `AddMapper` creates fresh profile instances, and tests do too.
- **[Namespace change breaks external `using`s]** → Only three files in `src` and three in `tests` use it, and the compiler finds them all.

## Migration Plan

1. Move and rewrite the mapper files, delete `MapperRegistry.cs`, drop `InternalsVisibleTo`.
2. Update the `using` lines and the tests.
3. `dotnet build EventsHub.slnx`, `dotnet test EventsHub.slnx`, start the API and call `PUT /api/v1/events` once with unchanged values.

Rollback is a revert of the change; no data or contract is affected.
