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

`Core/` contains a small in-house mapper. It copies the members of one object onto another and replaces the AutoMapper dependency this project used before.

| Type | Role |
|---|---|
| `IMapper` | What handlers inject. `Map<TDestination>(source)` creates a new object; `Map(source, destination)` copies onto an existing one and returns it. |
| `Profile` | Base class for profiles. Declare maps in the constructor with `CreateMap<TSource, TDestination>()`. |
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

### Rules and limits

- Members are matched by name (case-sensitive) between public readable properties on the source and public settable properties on the destination. Source members with no match are ignored.
- A matching name with incompatible types makes the application fail at startup, as does declaring the same map in two profiles.
- Mapping a pair that no profile declared throws `InvalidOperationException` and leaves the destination untouched.
- Values are copied by reference. This is safe for the string, `DateTime` and `bool` members of `Event`, but a profile that maps nested objects would share them between source and destination.
- There are no per-member rules (`ForMember`, `MapFrom`, `Ignore`), collection mapping or `IQueryable` projection yet. Add them in a new change.
- The unit tests are in `tests/EventsHub.UnitTests/Core/MapperTests.cs`.

## When you add code

Keep this README and the docs in sync:

1. Add new use cases to the table above.
2. If you add services that handlers need, register them in `src/EventsHub.Api/Program.cs`, and in `src/EventsHub.OpenApi/Program.cs` if Swagger UI needs to call those endpoints.
3. If you add NuGet packages, list them here with their purpose.
4. If you change the request flow, update [Docs/Architecture.md](../../Docs/Architecture.md): the component map, the request-flow diagram and the layering decision.

See [Docs/Architecture.md](../../Docs/Architecture.md) for how this project fits with the rest of the system.
