# EventsHub

ICI 2026 01

EventsHub is an events catalogue made of an ASP.NET Core (.NET 10) REST API backed by SQLite, and a React + Vite + MUI web frontend.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Node.js and npm
- Optional: `dotnet-ef` global tool for migrations (`dotnet tool install --global dotnet-ef`)
- Optional: [Bruno](https://www.usebruno.com/) for the integration tests

## Quick start

Start the API (it creates, migrates and seeds the SQLite database on first run):

```powershell
dotnet run --project src/EventsHub.Api
```

In a second terminal, start the frontend:

```powershell
cd web; npm install
cd EventsHub; npm install
npm run dev
```

Open https://localhost:3000. The API is at https://localhost:5001/api/v1/events.

Run the unit tests:

```powershell
dotnet test EventsHub.slnx
```

## Repository layout

| Path | Description |
|---|---|
| `src/EventsHub.Api` | REST API ([README](src/EventsHub.Api/README.md)) |
| `src/EventsHub.Domain` | Entities ([README](src/EventsHub.Domain/README.md)) |
| `src/EventsHub.Persistence` | EF Core context, migrations, seed data ([README](src/EventsHub.Persistence/README.md)) |
| `src/EventsHub.Application` | Reserved application layer, currently empty ([README](src/EventsHub.Application/README.md)) |
| `src/EventsHub.OpenApi`, `src/nswag`, `src/openapi` | OpenAPI document and C# client generation ([README](src/EventsHub.OpenApi/README.md)) |
| `web/EventsHub` | React frontend ([README](web/EventsHub/README.md)) |
| `tests/EventsHub.UnitTests` | NUnit tests ([README](tests/EventsHub.UnitTests/README.md)) |
| `tests/EventsHub.IntegrationTests` | Bruno API tests ([README](tests/EventsHub.IntegrationTests/README.md)) |
| `Docs/` | Architecture and course guides |

## Documentation

- [Architecture](Docs/Architecture.md): components, request flow, design decisions and known inconsistencies
- [OpenAPI setup](Docs/OpenApi%201.md)
- [DbContext fundamentals](Docs/DbContextFundamentals.md)
- [Git in practice](Docs/GitInPracticeGuide.md)
