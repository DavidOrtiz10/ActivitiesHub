# EventsHub.Application

The application layer between the API and the data/domain projects. It holds the use cases (MediatR commands and queries with their handlers) and the object mapper the handlers use.

## Project references

`EventsHub.Api` references only this project. It reaches `EventsHub.Domain` and `EventsHub.Persistence` transitively through it:

```
EventsHub.Api → EventsHub.Application → EventsHub.Persistence → EventsHub.Domain
                                      ↘ EventsHub.Domain
```

Do not remove the references to Domain or Persistence from this project: handlers use `AppDbContext` and `Event` directly, and the API compiles against them through this project.

## Use cases

Controllers send a request through `IMediator`; a handler in this project runs it with `AppDbContext`. Each use case is a static container class holding a `Command` or `Query` and its `Handler`.

| Folder | Use case | Request | Result |
|---|---|---|---|
| `Events/Queries` | `GetEventsList` | `Query` | `List<Event>` |
| `Events/Queries` | `GetEventQueries` | `Query { Id }` | `Event` |
| `Events/Commands` | `CreateEvent` | `Command { Event }` | `string` (new event id) |
| `Events/Commands` | `EditEvent` | `Command { Event }` | none |
| `Events/Commands` | `DeleteEvent` | `Command { Id }` | none |

Handlers receive their dependencies through a primary constructor and throw an `Exception` when the event does not exist (`GetEventQueries`, `EditEvent` and `DeleteEvent`).

MediatR is registered in `src/EventsHub.Api/Program.cs` by scanning this assembly, so a new handler is picked up without registration.

## Mapping

`Core/Mapping/` contains a small in-house mapper in the `EventsHub.Application.Core.Mapping` namespace. It copies the members of one object onto another and replaces the AutoMapper dependency this project used before.

| Type | Role |
|---|---|
| `IMapper` | What handlers inject. `Map<TDestination>(source)` creates a new object; `Map(source, destination)` copies onto an existing one and returns it. |
| `Profile` | Base class for profiles. Declare maps in the constructor with `CreateMap<TSource, TDestination>()`. |
| `IMappingExpression` | What `CreateMap` returns. `ForMember` overrides one destination member. |
| `Mapper` | The implementation. Built from profiles; validates and compiles every map in its constructor. |
| `MappingProfiles` | The project's profile. |
| `AddMapper(assemblies)` | DI extension that finds every `Profile` in the given assemblies and registers `IMapper` as a singleton. |

### How `Event -> Event` is declared

```csharp
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Event, Event>();
    }
}
```

`EditEvent.Handler` uses it to copy the event sent by the client onto the tracked entity, so EF Core sees only the changed values:

```csharp
mapper.Map(request.Event, @event);
```

### Adding a profile

1. Create a class in this project that derives from `Profile`, with a public parameterless constructor.
2. Call `CreateMap<TSource, TDestination>()` for each pair in its constructor.
3. Inject `IMapper` where you need it. No registration change is needed: `AddMapper` scans the assembly at startup.

### Per-member rules

A member can be computed from the source or ignored. A rule wins over name matching for that member:

```csharp
CreateMap<Event, EventSummary>()
    .ForMember(d => d.Location, o => o.MapFrom(s => s.City + ", " + s.Venue))
    .ForMember(d => d.Notes, o => o.Ignore());
```

`ForMember` must select a public writable property and must call exactly one of `MapFrom` or `Ignore`, otherwise the profile throws `ArgumentException`.

### Nested objects and collections

If a source member and the matching destination member have different types, the mapper uses the map declared for those types, in any profile and in any order. A collection member is mapped element by element, in order, through the map for the element types. A null nested source gives a null destination member.

### Rules and limits

- Members are matched by name (case-sensitive) between public readable properties on the source and public settable properties on the destination. Source members with no match are ignored.
- A matching name whose types are not assignable and that no override, nested map or collection map converts makes the application fail at startup. The error names the map and the member. Declaring the same map in two profiles, or mapping to a type with no public parameterless constructor, also fails at startup.
- Mapping a pair that no profile declared throws `InvalidOperationException` and leaves the destination untouched.
- Plain members are copied by reference. A nested member mapped through a map becomes a new instance: mapping onto an existing destination replaces the nested object instead of merging into it.
- A collection member maps only into a destination type that a `List<T>` is assignable to (`List<T>`, `IList<T>`, `ICollection<T>`, `IEnumerable<T>`, `IReadOnlyList<T>`). Arrays and sets are rejected at startup.
- Data that refers to itself (a cycle) is not detected and would recurse. There is no `IQueryable` projection.
- A profile instance belongs to one `Mapper`. `AddMapper` creates new profile instances itself.
- The unit tests are in `tests/EventsHub.UnitTests/Core/MapperTests.cs`.

## When you add code

Keep this README and the docs in sync:

1. Add new use cases to the table above.
2. If you add services that handlers need, register them in `src/EventsHub.Api/Program.cs`, and in `src/EventsHub.OpenApi/Program.cs` if Swagger UI needs to call those endpoints.
3. If you add NuGet packages, list them here with their purpose.
4. If you change the request flow, update [Docs/Architecture.md](../../Docs/Architecture.md): the component map, the request-flow diagram and the layering decision.

See [Docs/Architecture.md](../../Docs/Architecture.md) for how this project fits with the rest of the system.
