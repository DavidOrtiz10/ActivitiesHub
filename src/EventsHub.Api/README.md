# EventsHub.Api

ASP.NET Core (.NET 10) Web API that exposes EventsHub's events over HTTP. It is the only runtime backend: the web frontend and the Bruno integration tests both call it.

See [Docs/Architecture.md](../../Docs/Architecture.md) for how it fits with the other projects.

## Running

From the repository root:

```powershell
dotnet run --project src/EventsHub.Api
```

The API listens on **https://localhost:5001** (the only profile in `Properties/launchSettings.json`, environment `Development`). On startup it applies EF Core migrations and seeds sample data if the database is empty, so no manual database setup is needed.

The SQLite file `EventsHub.db` is created in the working directory, which is `src/EventsHub.Api/` when started with the command above. Delete it to start over with freshly seeded data.

## Configuration

| Setting | Where | Value |
|---|---|---|
| Connection string | `appsettings.json` → `ConnectionStrings:SqliteConnection` | `Data Source=EventsHub.db` |
| CORS origins | `Program.cs` | `http://localhost:3000`, `https://localhost:3000` |
| URL | `Properties/launchSettings.json` | `https://localhost:5001` |

If the frontend runs on a different port, add it to the `WithOrigins(...)` call in `Program.cs`.

## Endpoints

All routes are prefixed with `api/v1/` by `EventsHubBaseController`.

| Method | Route | Action | Responses |
|---|---|---|---|
| GET | `/api/v1/events` | `EventsController.GetEventsAsync` | `200` with an array of events |
| GET | `/api/v1/events/{id}` | `EventsController.GetEventDetailAsync` | `200` with the event; `404` with `"The event was not found"` |
| GET | `/api/v1/weatherforecast` | `WeatherForecastController.Get` | `200` with 5 random forecasts (ASP.NET template sample) |

Example event:

```json
{
  "id": "0998ca40-78fc-4968-9a08-6c4d09635dc1",
  "title": "Future Event 1",
  "date": "2026-11-01T12:00:00",
  "description": "Event 1 months in future",
  "category": "drinks",
  "isCancelled": false,
  "city": "Guanajuato, Guanajuato",
  "venue": "Callejón del Beso",
  "latitude": "21.0190",
  "longitude": "-101.2574"
}
```

This API does not serve Swagger. To browse or regenerate the OpenAPI document, use [EventsHub.OpenApi](../EventsHub.OpenApi/README.md).

## Adding an endpoint

1. Create a controller in `Controllers/` that inherits `EventsHubBaseController`. You get the `[ApiController]` attribute and the `api/v1/[controller]` route automatically.
2. Inject `AppDbContext` through the primary constructor, as `EventsController` does.
3. Use `async` actions with an `Async` suffix that return `ActionResult<T>`.
4. If the controller needs a new dependency, register it in `Program.cs`. Register it in `src/EventsHub.OpenApi/Program.cs` too if you want to call it from the Swagger UI.
5. Add unit tests in `tests/EventsHub.UnitTests/Controllers/`, and Bruno requests in `tests/EventsHub.IntegrationTests/`.

## Database migrations

Migrations live in `EventsHub.Persistence`, but this project is the startup project (it holds the connection string and `Microsoft.EntityFrameworkCore.Design`). See the [Persistence README](../EventsHub.Persistence/README.md#migrations) for the commands.

## Tests

- Unit tests: `dotnet test tests/EventsHub.UnitTests` ([README](../../tests/EventsHub.UnitTests/README.md))
- Integration tests: Bruno collection, which requires this API to be running ([README](../../tests/EventsHub.IntegrationTests/README.md))
