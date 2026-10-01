# EventsHub.Persistence

Data access for EventsHub, built on Entity Framework Core 10 with the SQLite provider. It owns the database context, schema migrations and sample seed data. It references only `EventsHub.Domain`.

## Contents

| File | Purpose |
|---|---|
| `AppDbContext.cs` | EF Core context exposing `DbSet<Event> Events`. Options (provider, connection string) are supplied by the host. |
| `DbInitializer.cs` | `SeedDataAsync(context)` inserts 10 sample events (5 in the past, 5 in the future, at Mexican venues) only if the `Events` table is empty. |
| `Migrations/` | Code-first migrations. Currently only `InitialCreate`. |

`AppDbContext` does not configure itself; each host does:

- **EventsHub.Api** registers it with `UseSqlite(ConnectionStrings:SqliteConnection)`, then migrates and seeds on startup.
- **EventsHub.UnitTests** builds it manually with `UseSqlite("Data source=EventsHub.db")`, then migrates and seeds once per test run.

Event dates are seeded relative to `DateTime.Now` and ids are random GUIDs, so every new database has different ids and dates.

## Migrations

The EF Core CLI is a global tool (it is not in the repo's local tool manifest). Install it once:

```powershell
dotnet tool install --global dotnet-ef
```

Run these from the repository root. `EventsHub.Api` is the startup project because it has the connection string and the `Microsoft.EntityFrameworkCore.Design` package.

```powershell
# Add a migration after changing an entity or AppDbContext
dotnet ef migrations add <Name> --project src/EventsHub.Persistence --startup-project src/EventsHub.Api

# List migrations and whether they are applied
dotnet ef migrations list --project src/EventsHub.Persistence --startup-project src/EventsHub.Api

# Apply migrations without starting the API
dotnet ef database update --project src/EventsHub.Persistence --startup-project src/EventsHub.Api
```

You don't need to run `database update` in normal development: the API applies pending migrations every time it starts.

## Resetting data

Seeding only runs against an empty table, so changes to `DbInitializer` won't appear in an existing database. Delete `EventsHub.db` (in `src/EventsHub.Api/` when running the API, or in the test output folder for unit tests) and start again.
