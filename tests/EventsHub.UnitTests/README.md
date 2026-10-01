# EventsHub.UnitTests

NUnit 4 tests for the API controllers. Controllers are instantiated directly, with no web host, and run against a real SQLite database.

## Running

From the repository root:

```powershell
# All tests in the solution
dotnet test EventsHub.slnx

# This project only
dotnet test tests/EventsHub.UnitTests

# A single test
dotnet test tests/EventsHub.UnitTests --filter "FullyQualifiedName~EventsControllerTests.GetEventDetailAsync_WhenEventDoesNotExist_ReturnsNotFound"

# A whole fixture
dotnet test tests/EventsHub.UnitTests --filter "FullyQualifiedName~EventsControllerTests"
```

Coverage can be collected with the bundled `coverlet.collector`: `dotnet test tests/EventsHub.UnitTests --collect:"XPlat Code Coverage"`.

## How the database is set up

`GlobalTestSetup` is an NUnit `[SetUpFixture]` at the root namespace, so it runs once for the whole test run:

1. Builds an `AppDbContext` with `UseSqlite("Data source=EventsHub.db")`. The file is created in the test output folder (`bin/Debug/net10.0/`).
2. Applies migrations and runs `DbInitializer.SeedDataAsync`, the same as the API does on startup.
3. Exposes the context as the static `GlobalTestSetup.AppDbContext`, and disposes it after all tests finish.

Every test shares this one context and file. The current tests only read data. A test that writes data would affect the others, and the changes would stay in the file between runs. Delete the `.db` file in the output folder to reset it.

## Writing tests

- Create the controller in `[SetUp]` with `GlobalTestSetup.AppDbContext`.
- Name tests `Method_WhenCondition_ExpectedResult` and use `// Arrange`, `// Act`, `// Assert` comments.
- For `ActionResult<T>`, assert on `result.Value` for success, and on `result.Result` (e.g. `NotFoundObjectResult`) for error responses.
- Derive expected values from the database (e.g. `Events.CountAsync()`, `Events.FirstAsync()`), not from hard-coded ids, because seeded ids are random.
- Place tests under a folder that mirrors the code under test (`Controllers/EventsControllerTests.cs`).
