# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

EventsHub is a course project (ICI 2026 01): an ASP.NET Core (.NET 10) Web API backed by SQLite/EF Core, plus a React + Vite + MUI frontend in `web/EventsHub`. The dev shell is PowerShell on Windows.

## Commands

Backend (run from repo root; solution file is `EventsHub.slnx`):

```powershell
dotnet build EventsHub.slnx
dotnet run --project src/EventsHub.Api            # https://localhost:5001
dotnet test EventsHub.slnx
dotnet test tests/EventsHub.UnitTests --filter "FullyQualifiedName~EventsControllerTests.GetEventsAsync_WhenEventsExists_ReturnsAllEvents"
dotnet ef migrations add <Name> --project src/EventsHub.Persistence --startup-project src/EventsHub.Api
```

Frontend (from `web/EventsHub`):

```powershell
npm run dev      # Vite on port 3000 with HTTPS via vite-plugin-mkcert
npm run build    # tsc -b && vite build
npm run lint
```

Note: `axios` is declared in `web/package.json` (parent folder), not in `web/EventsHub/package.json`; it resolves from `web/node_modules`, so `npm install` must be run in both `web/` and `web/EventsHub/`.

## Architecture

Projects under `src/` and their references:

- `EventsHub.Domain` — entities (`Event`, string GUID `Id`). No dependencies.
- `EventsHub.Persistence` — `AppDbContext`, EF migrations, `DbInitializer.SeedDataAsync` (seeds 10 events only if the table is empty). References Domain.
- `EventsHub.Application` — currently empty; only exists to reference Domain + Persistence.
- `EventsHub.Api` — controllers inject `AppDbContext` directly (no service/repository layer). References Application. All controllers inherit `EventsHubBaseController`, which sets `[ApiController]` and the route `api/v1/[controller]`. On startup `Program.cs` applies migrations and seeds the DB (`Data Source=EventsHub.db`, relative to the working directory). CORS allows only `http(s)://localhost:3000` (the Vite dev server).
- `EventsHub.OpenApi` — a separate NSwag host that loads the Api assembly's controllers as an application part to serve Swagger (`/swagger/EventsHub/swagger.json`, ports 5010/5011). It does not register `AppDbContext`, so controllers that need DI dependencies require registering them here too.

OpenAPI/client generation pipeline (full steps in `Docs/OpenApi 1.md`): run the OpenApi host → download the swagger JSON to `src/openapi/EventsHub.v1.json` → `dotnet tool restore; dotnet tool run nswag run src/nswag/EventsHub.nswag`. The NSwag output path is relative to `src/nswag/`, so the generated client lands in `src/src/EventsHub.OpenApi/Generated/`. Never hand-edit the generated JSON or `.generated.cs`.

Frontend: `App.tsx` fetches `https://localhost:5001/api/v1/events` with axios (URL hard-coded). The `Activity` type in `src/lib/types/index.d.ts` is a global ambient type (no import needed) that mirrors the backend `Event` JSON (camelCase). React Compiler is enabled via the Babel preset in `vite.config.ts`.

## Tests

- `tests/EventsHub.UnitTests` (NUnit 4): `GlobalTestSetup` (`[SetUpFixture]`) creates a single shared `AppDbContext` against a real SQLite file (`EventsHub.db` in the test output dir), runs migrations and seeds it. Tests instantiate controllers directly with `GlobalTestSetup.AppDbContext`, and assert on `ActionResult<T>.Value` / `.Result`.
- `tests/EventsHub.IntegrationTests` is a Bruno (OpenCollection YAML) collection, not a .NET project. It targets `https://localhost:5001/api/v1` (`environments/local.yml`) and needs the API running. Some requests hard-code event IDs from a seeded database.

## Conventions

- Commit messages follow `feat(<branch>) - <description>` (e.g. `feat(parcial02) - add unit tests`); work happens on assignment branches (`parcial02`) merged into `main`.
- Test names use `Method_WhenCondition_ExpectedResult` with Arrange/Act/Assert comments.
