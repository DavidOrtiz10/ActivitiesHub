# EventsHub.Domain

Entity classes for EventsHub. This project has no dependencies, and the other backend projects reference it.

## `Event`

| Property | Type | Required | Default |
|---|---|---|---|
| `Id` | `string` | — | New GUID string |
| `Title` | `string` | yes | — |
| `Date` | `DateTime` | — | `DateTime.Now` |
| `Description` | `string` | yes | — |
| `Category` | `string` | yes | — |
| `IsCancelled` | `bool` | — | `false` |
| `City` | `string` | yes | — |
| `Venue` | `string` | yes | — |
| `Latitude` | `string` | yes | — |
| `Longitude` | `string` | yes | — |

"Required" means a C# `required` member: you must set it in object initializers.

The API returns `Event` directly to clients (there are no DTOs), so the property names here are the camelCase JSON field names the frontend receives. Renaming a property changes the API contract and needs a new EF Core migration (see [Persistence](../EventsHub.Persistence/README.md#migrations)).

The frontend's matching type is `Activity` in `web/EventsHub/src/lib/types/index.d.ts`. It types `latitude` and `longitude` as `number`, unlike `string` here.
