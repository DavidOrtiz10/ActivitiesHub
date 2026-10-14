# Graph Report - EventsHub  (2026-10-14)

## Corpus Check
- Corpus is ~40,487 words - fits in a single context window. You may not need a graph.

## Summary
- 538 nodes · 807 edges · 36 communities (27 shown, 9 thin omitted)
- Extraction: 92% EXTRACTED · 8% INFERRED · 0% AMBIGUOUS · INFERRED: 62 edges (avg confidence: 0.83)
- Token cost: 491,701 input · 0 output

## Community Hubs (Navigation)
- CQRS Handlers y Controllers
- Cliente OpenAPI Generado
- Docs de Proyecto y Arquitectura
- Namespaces y Proyectos .NET
- Solución y Paquetes NuGet
- Tests de Integración Bruno
- Controllers Base y Mediator
- Fundamentos Clean Architecture
- Config TS App
- Migraciones EF Core
- DevDependencies Frontend
- Entidad Event y Modelos
- Config TS Node
- package.json Frontend
- Fundamentos de Testing
- Fundamentos CQRS
- LaunchSettings Api
- LaunchSettings OpenApi
- App React y Entrada
- Dependencias Runtime Frontend
- Sprite de Íconos SVG
- Config ESLint
- Config Vite
- Scripts npm
- Axios en web/
- Event Sourcing
- Logo Vite
- tsconfig Raíz
- Patrón Result
- Favicon
- Imagen Hero
- Logo React
- Tipo Activity
- Patrón Options
- Carpeta Bruno Events
- Carpeta Bruno WeatherForecast

## God Nodes (most connected - your core abstractions)
1. `Event` - 26 edges
2. `EventsRpcClient` - 20 edges
3. `WeatherForecastRpcClient` - 19 edges
4. `compilerOptions` - 18 edges
5. `EventsHub Architecture Document` - 18 edges
6. `compilerOptions` - 15 edges
7. `AppDbContext` - 14 edges
8. `Event` - 13 edges
9. `EventsHub.Persistence` - 11 edges
10. `EventsController` - 9 edges

## Surprising Connections (you probably didn't know these)
- `CLAUDE.md (project guidance)` --semantically_similar_to--> `EventsHub Architecture Document`  [INFERRED] [semantically similar]
  CLAUDE.md → Docs/Architecture.md
- `API Startup (migrate + seed + CORS)` --semantically_similar_to--> `CORS Origins localhost:3000`  [INFERRED] [semantically similar]
  Docs/Architecture.md → src/EventsHub.Api/README.md
- `Generation Pipeline (run host -> fetch JSON -> nswag run)` --semantically_similar_to--> `Regenerating Document and Client`  [INFERRED] [semantically similar]
  Docs/guides/OpenApi 1.md → src/EventsHub.OpenApi/README.md
- `vite-plugin-mkcert HTTPS` --semantically_similar_to--> `ASP.NET Core dev certificate trust`  [INFERRED] [semantically similar]
  web/EventsHub/README.md → tests/EventsHub.IntegrationTests/README.md
- `Current State and Known Inconsistencies` --implements--> `Phase 1 Discovery (detect stack from real signals)`  [INFERRED]
  Docs/Architecture.md → .claude/skills/architecture-docs/SKILL.md

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **OpenAPI Document and C# Client Generation Flow** — docs_guides_openapi_1_openapi_host_program, docs_guides_openapi_1_nswag_config, docs_guides_openapi_1_nswag_local_tool, docs_guides_openapi_1_generation_pipeline, src_eventshub_openapi_readme_generated_rpc_clients [EXTRACTED 1.00]
- **EventsHub Key Architectural Decisions** — docs_architecture_layered_by_project, docs_architecture_sqlite_file_database, docs_architecture_separate_documentation_host, docs_architecture_url_based_versioning, docs_architecture_real_database_in_unit_tests [EXTRACTED 1.00]
- **CQRS Distributed Reliability Patterns** — docs_fundamentals_cqrsfundamentals_eventual_consistency, docs_fundamentals_cqrsfundamentals_outbox_pattern, docs_fundamentals_cqrsfundamentals_sagas, docs_fundamentals_cqrsfundamentals_idempotency_concurrency, docs_fundamentals_cqrsfundamentals_projections [INFERRED 0.85]
- **Events CRUD integration requests against /Events** — tests_eventshub_integrationtests_events_events___list___200, tests_eventshub_integrationtests_events_events___get___200, tests_eventshub_integrationtests_events_events___get___404, tests_eventshub_integrationtests_events_events___create__200, tests_eventshub_integrationtests_events_events___edit___204, tests_eventshub_integrationtests_events_events___delete__200 [EXTRACTED 1.00]
- **Mutation requests reusing Get-404 test script** — tests_eventshub_integrationtests_events_events___create__200, tests_eventshub_integrationtests_events_events___edit___204, tests_eventshub_integrationtests_events_events___delete__200, tests_eventshub_integrationtests_events_events___get___404 [INFERRED 0.85]
- **Social/brand link icons (Vite template)** — web_eventshub_public_icons_bluesky_icon, web_eventshub_public_icons_discord_icon, web_eventshub_public_icons_github_icon, web_eventshub_public_icons_x_icon [INFERRED 0.85]

## Communities (36 total, 9 thin omitted)

### Community 0 - "CQRS Handlers y Controllers"
Cohesion: 0.06
Nodes (32): EventsController, Command, Event, CreateEvent, Handler, Command, Id, Handler (+24 more)

### Community 1 - "Cliente OpenAPI Generado"
Cohesion: 0.07
Nodes (21): EventsHub.OpenApi.Client, ApiException, ApiException, Result, Headers, Response, StatusCode, DateFormatConverter (+13 more)

### Community 2 - "Docs de Proyecto y Arquitectura"
Cohesion: 0.06
Nodes (46): .claude/CLAUDE.md (graphify skill pointer), architecture-docs Skill, CLAUDE.md (project guidance), Commit Convention feat(<branch>) - <description>, EventsHub Architecture Document, API Startup (migrate + seed + CORS), Brittle Bruno Test with Fixed Event Id, Activity vs Event Model Divergence (lat/long types) (+38 more)

### Community 3 - "Namespaces y Proyectos .NET"
Cohesion: 0.10
Nodes (12): EventsHub.Domain, EventsHub.Application.Events.Queries, EventsHub.Application.Events.Commands, EventsHub.UnitTests, EventsHub.Persistence, EventsHub.Application.Core, MappingProfiles, DeleteEvent (+4 more)

### Community 4 - "Solución y Paquetes NuGet"
Cohesion: 0.07
Nodes (26): AutoMapper (13.0.1), coverlet.collector (6.0.4), MediatR (14.2.0), Microsoft.AspNetCore.Mvc.NewtonsoftJson (10.0.11), Microsoft.AspNetCore.OpenApi (10.0.11), Microsoft.EntityFrameworkCore.Design (10.0.11), Microsoft.EntityFrameworkCore.Sqlite (10.0.11), Microsoft.NET.Test.Sdk (17.14.0) (+18 more)

### Community 5 - "Tests de Integración Bruno"
Cohesion: 0.12
Nodes (16): baseUrl (local environment), Events - Create- 200 (Bruno request), Events - Delete- 200 (Bruno request), Events - Edit - 204 (Bruno request), Events - Get - 200 (Bruno request), Events - Get - 404 (Bruno request), Events - List - 200 (Bruno request), EventsHub.IntegrationTests OpenCollection (+8 more)

### Community 6 - "Controllers Base y Mediator"
Cohesion: 0.10
Nodes (11): EventsHub.Api.Controllers, EventsHub.Api, EventsHub.UnitTests.Controllers, EventsHubBaseController, Mediator, WeatherForecastController, WeatherForecast, Date (+3 more)

### Community 7 - "Fundamentos Clean Architecture"
Cohesion: 0.10
Nodes (21): Clean Architecture Fundamentals Guide, Architecture Tests (NetArchTest/ArchUnitNET), Clean Architecture (Robert C. Martin), Dependency Injection and Composition Root, Hexagonal Architecture (Ports & Adapters), IEventRepository (example abstraction), Onion Architecture, Repository and Unit of Work (+13 more)

### Community 8 - "Config TS App"
Cohesion: 0.10
Nodes (19): compilerOptions, allowArbitraryExtensions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection (+11 more)

### Community 9 - "Migraciones EF Core"
Cohesion: 0.16
Nodes (3): EventsHub.Persistence.Migrations, InitialCreate, AppDbContextModelSnapshot

### Community 10 - "DevDependencies Frontend"
Cohesion: 0.11
Nodes (18): devDependencies, @babel/core, babel-plugin-react-compiler, eslint, @eslint/js, eslint-plugin-react-hooks, eslint-plugin-react-refresh, globals (+10 more)

### Community 11 - "Entidad Event y Modelos"
Cohesion: 0.12
Nodes (16): Event, Category, City, Date, Description, Id, IsCancelled, Latitude (+8 more)

### Community 12 - "Config TS Node"
Cohesion: 0.12
Nodes (16): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, noEmit, noFallthroughCasesInSwitch (+8 more)

### Community 13 - "package.json Frontend"
Cohesion: 0.13
Nodes (14): @babel/core, babel-plugin-react-compiler, @emotion/react, @emotion/styled, @mui/icons-material, @types/babel__core, @types/node, @types/react (+6 more)

### Community 14 - "Fundamentos de Testing"
Cohesion: 0.18
Nodes (13): Test Naming Method_WhenCondition_ExpectedResult, Shared AppDbContext Test State, Integration Testing with WebApplicationFactory, Testing with DbContext (InMemory vs SQLite vs real DB), Software Testing Fundamentals Guide, Arrange-Act-Assert Pattern, FIRST Principles for Good Tests, MethodUnderTest_Scenario_ExpectedResult Naming (+5 more)

### Community 15 - "Fundamentos CQRS"
Cohesion: 0.17
Nodes (11): CQRS: Commands vs Queries (Clean Arch topic), Validation with FluentValidation, MediatR and Pipeline Behaviors, Feature-based Folders (Vertical Slices), CQRS Fundamentals Guide, Commands, Queries, and Events, CQRS (Greg Young), CQRS in Web APIs (HTTP verb mapping) (+3 more)

### Community 16 - "LaunchSettings Api"
Cohesion: 0.20
Nodes (9): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, profiles, https (+1 more)

### Community 17 - "LaunchSettings OpenApi"
Cohesion: 0.22
Nodes (8): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, environmentVariables, launchBrowser, profiles, EventsHub.OpenApi, $schema

### Community 18 - "App React y Entrada"
Cohesion: 0.32
Nodes (5): @fontsource/roboto, @mui/material, react, react-dom, App()

### Community 19 - "Dependencias Runtime Frontend"
Cohesion: 0.25
Nodes (8): dependencies, @emotion/react, @emotion/styled, @fontsource/roboto, @mui/icons-material, @mui/material, react, react-dom

### Community 20 - "Sprite de Íconos SVG"
Cohesion: 0.29
Nodes (8): icons.svg (SVG sprite sheet), bluesky-clip clipPath, bluesky-icon, discord-icon, documentation-icon, github-icon, social-icon, x-icon

### Community 21 - "Config ESLint"
Cohesion: 0.29
Nodes (6): eslint, @eslint/js, eslint-plugin-react-hooks, eslint-plugin-react-refresh, globals, typescript-eslint

### Community 22 - "Config Vite"
Cohesion: 0.40
Nodes (4): @rolldown/plugin-babel, vite, vite-plugin-mkcert, @vitejs/plugin-react

### Community 23 - "Scripts npm"
Cohesion: 0.40
Nodes (5): scripts, build, dev, lint, preview

### Community 24 - "Axios en web/"
Cohesion: 0.50
Nodes (3): axios, dependencies, axios

### Community 25 - "Event Sourcing"
Cohesion: 0.67
Nodes (3): Event Sourcing, Eventual Consistency, Projections and Read Model Rebuilding

### Community 26 - "Logo Vite"
Cohesion: 0.67
Nodes (3): Prefers-Color-Scheme Dark Mode Styling, Vite Logo (SVG), Vite Build Tool

## Ambiguous Edges - Review These
- `Events - Create- 200 (Bruno request)` → `Events - Get - 404 (Bruno request)`  [AMBIGUOUS]
  tests/EventsHub.IntegrationTests/Events/Events - Create- 200.yml · relation: semantically_similar_to

## Knowledge Gaps
- **129 isolated node(s):** `Mediator`, `net10.0`, `Microsoft.AspNetCore.OpenApi (10.0.11)`, `Microsoft.EntityFrameworkCore.Design (10.0.11)`, `Microsoft.NET.Sdk.Web` (+124 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 247 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **9 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Event` connect `CQRS Handlers y Controllers` to `Migraciones EF Core`, `Namespaces y Proyectos .NET`?**
  _High betweenness centrality (0.041) - this node is a cross-community bridge._
- **What connects `Mediator`, `net10.0`, `Microsoft.AspNetCore.OpenApi (10.0.11)` to the rest of the system?**
  _129 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `CQRS Handlers y Controllers` be split into smaller, more focused modules?**
  _Cohesion score 0.056535504296698326 - nodes in this community are weakly interconnected._
- **Why does `EventsHub Architecture Document` connect `Docs de Proyecto y Arquitectura` to `Fundamentos de Testing`?**
  _High betweenness centrality (0.018) - this node is a cross-community bridge._
- **Should `Cliente OpenAPI Generado` be split into smaller, more focused modules?**
  _Cohesion score 0.0701484895033282 - nodes in this community are weakly interconnected._
- **Why does `Event` connect `Entidad Event y Modelos` to `Cliente OpenAPI Generado`?**
  _High betweenness centrality (0.017) - this node is a cross-community bridge._

### Low-confidence Hints
_AMBIGUOUS edges — the extractor was unsure. Verify before acting on these._

- **What is the exact relationship between `Events - Create- 200 (Bruno request)` and `Events - Get - 404 (Bruno request)`?**
  _Edge tagged AMBIGUOUS (relation: semantically_similar_to) - confidence is low._