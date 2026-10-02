# EventsHub.Application

The application layer between the API and the data/domain projects. **It currently contains no code**: the project only holds `EventsHub.Application.csproj` with references to `EventsHub.Domain` and `EventsHub.Persistence`.

## Why it exists today

`EventsHub.Api` references only this project. It reaches `EventsHub.Domain` and `EventsHub.Persistence` transitively through it:

```
EventsHub.Api → EventsHub.Application → EventsHub.Persistence → EventsHub.Domain
                                      ↘ EventsHub.Domain
```

Do not delete this project or its references because it looks empty. If you do, the API loses access to `AppDbContext` and `Event` and stops compiling. If you remove it on purpose, add direct references from `EventsHub.Api` to Domain and Persistence first.

## Current state

Controllers in `EventsHub.Api` inject `AppDbContext` directly and return `Event` entities as-is. Querying, "not found" handling and response shaping all happen in the controllers. Nothing in this project is used at runtime or in tests.

## What belongs here

Code added to this project should be application logic that sits between HTTP and persistence, for example:

- **Use cases or services**, such as listing events or getting event details, which controllers call instead of querying `AppDbContext` themselves.
- **DTOs and mapping** between `Event` and the shapes the API sends and receives, so the domain entity is no longer the public contract.
- **Validation** of incoming data, for example when create/update endpoints are added.
- **Interfaces** that the API depends on, so controllers can be unit-tested without a database.

API concerns (routing, HTTP status codes, CORS) stay in `EventsHub.Api`. Database concerns (`DbContext`, migrations, seeding) stay in `EventsHub.Persistence`.

## When you add code

Keep this README and the docs in sync:

1. Update the **Current state** section above, and list the main services or types this project provides.
2. If you register services, document where they are registered. Register them in `src/EventsHub.Api/Program.cs`, and in `src/EventsHub.OpenApi/Program.cs` if Swagger UI needs to call those endpoints.
3. If you add NuGet packages (e.g. validation or mapping libraries), list them here with their purpose.
4. If controllers change to use this layer, update [Docs/Architecture.md](../../Docs/Architecture.md): the component map, the request-flow sequence diagram and the "Layered by project, not by behavior" decision.
5. If you add tests for this project, add them to `EventsHub.slnx` and describe how to run them here.

See [Docs/Architecture.md](../../Docs/Architecture.md) for how this project fits with the rest of the system.
