# EventsHub.IntegrationTests

HTTP-level tests for the running API, written as a [Bruno](https://www.usebruno.com/) collection in the OpenCollection YAML format. This is not a .NET project and is not part of `EventsHub.slnx`, so `dotnet test` does not run it.

## Running

1. Start the API: `dotnet run --project src/EventsHub.Api` (serves `https://localhost:5001`).
2. In Bruno, open this folder as a collection (`opencollection.yml` is the collection file).
3. Select the **local** environment, which sets `baseUrl` to `https://localhost:5001/api/v1`.
4. Run the whole collection or individual requests. Each request has a test script that checks the status code.

The API uses the ASP.NET Core development certificate. If Bruno rejects it, trust the certificate with `dotnet dev-certs https --trust`, or turn off SSL verification in Bruno's settings for local runs.

## Requests

| Folder | Request | Call | Expects |
|---|---|---|---|
| Events | Events - List - 200 | `GET {{baseUrl}}/Events` | `200` |
| Events | Events - Get - 200 | `GET {{baseUrl}}/Events/:eventid` | `200` |
| Events | Events - Get - 404 | `GET {{baseUrl}}/Events/:eventid` with an unknown id | `404` |
| WeatherForecast | WeatherForecast - List - 200 | `GET {{baseUrl}}/WeatherForecast` | `200` |

**Events - Get - 200** uses a fixed event id. The API seeds events with random GUIDs, so this request only passes against the database it was written for. Against a freshly seeded database, copy an id from the `Events - List - 200` response into the `eventid` path parameter.

## Adding requests

Create the request in Bruno inside the matching folder (or a new folder per controller). Name it `<Controller> - <Action> - <ExpectedStatus>`, use `{{baseUrl}}` in the URL, and add a test script that asserts on the status code. Bruno saves it as a `.yml` file here.
