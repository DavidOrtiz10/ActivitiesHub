# EventsHub.OpenApi

A standalone ASP.NET Core host whose only job is to describe the EventsHub API. It loads `EventsHub.Api`'s controllers as an MVC application part and serves an OpenAPI 3 document and Swagger UI with NSwag. It is not part of the runtime system, and the frontend does not call it.

Related folders:

| Path | Contents | Edit by hand? |
|---|---|---|
| `src/EventsHub.OpenApi/` | This host (`Program.cs`, `launchSettings.json`) | Yes |
| `src/nswag/EventsHub.nswag` | NSwag code-generation config | Only for codegen changes |
| `src/openapi/EventsHub.v1.json` | Generated OpenAPI document | **No** |
| `src/src/EventsHub.OpenApi/Generated/EventsHubRpcClient.generated.cs` | Generated C# client (`EventsRpcClient`, `WeatherForecastRpcClient`) | **No** |

Background on how this setup was created is in [Docs/OpenApi 1.md](../../Docs/OpenApi%201.md).

## Browsing the API

```powershell
dotnet run --project src/EventsHub.OpenApi
```

This opens Swagger UI on https://localhost:5011 (http://localhost:5010). The document's name is `EventsHub`, served at `/swagger/EventsHub/swagger.json`, and its title is `EventsHubV1`.

`AppDbContext` is not registered in this host, so "Try it out" on `Events` endpoints fails. Use the real API on `https://localhost:5001` to call endpoints.

## Regenerating the document and client

Do this after adding or changing controllers or actions in `EventsHub.Api`. Run every step from the repository root.

1. Restore the local NSwag tool (version 14.7.1, from `.config/dotnet-tools.json`):

   ```powershell
   dotnet tool restore
   ```

2. Start the host without its launch profile, on a fixed HTTP port:

   ```powershell
   dotnet run --project src/EventsHub.OpenApi/EventsHub.OpenApi.csproj --no-launch-profile --urls http://127.0.0.1:5011
   ```

   Wait until it prints `Now listening on: http://127.0.0.1:5011`.

3. In another terminal, download the document:

   ```powershell
   Invoke-WebRequest -Uri http://127.0.0.1:5011/swagger/EventsHub/swagger.json -OutFile src/openapi/EventsHub.v1.json
   ```

4. Stop the host so it doesn't lock its binaries:

   ```powershell
   Get-Process -Name "EventsHub.OpenApi" | Stop-Process -Force
   ```

5. Generate the C# client:

   ```powershell
   dotnet tool run nswag run src/nswag/EventsHub.nswag
   ```

6. Rebuild to confirm everything compiles:

   ```powershell
   dotnet build EventsHub.slnx
   ```

### Notes

- The `runtime` in `EventsHub.nswag` (`Net100`) must match the SDK that runs NSwag (.NET 10).
- Paths in `EventsHub.nswag` are relative to `src/nswag/`. Because of this, `output: ../src/EventsHub.OpenApi/...` resolves to `src/src/EventsHub.OpenApi/Generated/`, outside this project. The generated client is therefore not compiled by any project. To compile it here, change `output` to `../EventsHub.OpenApi/Generated/EventsHubRpcClient.generated.cs`.
- The generated client uses Newtonsoft.Json and one client class per controller (`{controller}RpcClient`, namespace `EventsHub.OpenApi.Client`).
- If a new controller depends on a service, register that service in this host's `Program.cs` as well, or Swagger UI calls to it will fail.
