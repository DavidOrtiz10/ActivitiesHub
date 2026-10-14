# Graph Report - EventsHub  (2026-10-14)

## Corpus Check
- 90 files · ~72,582 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 6, .css 2, .nswag 1)

## Summary
- 695 nodes · 1100 edges · 63 communities (41 shown, 22 thin omitted)
- Extraction: 90% EXTRACTED · 10% INFERRED · 0% AMBIGUOUS · INFERRED: 113 edges (avg confidence: 0.87)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `7704353f`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Event
- Task
- EventsHub Architecture Document
- AppDbContext
- EventsHub.UnitTests.csproj
- EventsHub.IntegrationTests README
- WeatherForecast
- The Dependency Rule
- compilerOptions
- 20260907164228_InitialCreate.Designer.cs
- devDependencies
- Event
- compilerOptions
- EventsHub/package.json
- Software Testing Fundamentals Guide
- CQRS (Greg Young)
- https
- EventsHub.OpenApi
- main.tsx
- dependencies
- icons.svg (SVG sprite sheet)
- eslint.config.js
- vite.config.ts
- scripts
- axios
- Projections and Read Model Rebuilding
- Vite Logo (SVG)
- tsconfig.json
- Result Pattern vs Exceptions
- favicon.svg (Vite logo favicon)
- Hero Image (layered isometric tiles)
- React Logo (react.svg)
- index.d.ts
- Configuration and the Options Pattern
- Events Bruno folder
- WeatherForecast Bruno folder
- Profile
- MapperTests
- ADDED Requirements
- MapperRegistry
- openspec-explore/SKILL.md
- Layered by Project, Not by Behavior
- IMapper
- explore.md
- Graphify (graphifyy knowledge graph tool)
- Current State and Known Inconsistencies
- OpenAPI Manual Setup Guide (OpenApi 1.md)
- CLAUDE.md (project guidance)
- EventsHub Web README
- EventsHub.UnitTests README
- RequiredPersonCopy
- Coordinates
- NumericCoordinates

## God Nodes (most connected - your core abstractions)
1. `Event` - 26 edges
2. `EventsHub Architecture Document` - 21 edges
3. `EventsRpcClient` - 20 edges
4. `MapperTests` - 20 edges
5. `Profile` - 19 edges
6. `WeatherForecastRpcClient` - 19 edges
7. `compilerOptions` - 18 edges
8. `compilerOptions` - 15 edges
9. `AppDbContext` - 14 edges
10. `IMapper` - 13 edges

## Surprising Connections (you probably didn't know these)
- `Context` --references--> `Handler`  [INFERRED]
  openspec/changes/replace-automapper-custom-mapper/design.md → src/EventsHub.Application/Events/Commands/EditEvent.cs
- `1. Mapper core` --references--> `EventsController`  [INFERRED]
  openspec/changes/replace-automapper-custom-mapper/tasks.md → src/EventsHub.Api/Controllers/EventsController.cs
- `2. Public surface mirrors AutoMapper's subset in use` --references--> `IMapper`  [INFERRED]
  openspec/changes/replace-automapper-custom-mapper/design.md → src/EventsHub.Application/Core/IMapper.cs
- `4. Immutable registry shared as a singleton` --references--> `IMapper`  [INFERRED]
  openspec/changes/replace-automapper-custom-mapper/design.md → src/EventsHub.Application/Core/IMapper.cs
- `What Changes` --references--> `IMapper`  [INFERRED]
  openspec/changes/replace-automapper-custom-mapper/proposal.md → src/EventsHub.Application/Core/IMapper.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Events CRUD integration requests against /Events** — tests_eventshub_integrationtests_events_events___list___200, tests_eventshub_integrationtests_events_events___get___200, tests_eventshub_integrationtests_events_events___get___404, tests_eventshub_integrationtests_events_events___create__200, tests_eventshub_integrationtests_events_events___edit___204, tests_eventshub_integrationtests_events_events___delete__200 [EXTRACTED 1.00]
- **EventsHub Key Architectural Decisions** — docs_architecture_layered_by_project, docs_architecture_sqlite_file_database, docs_architecture_separate_documentation_host, docs_architecture_url_based_versioning, docs_architecture_real_database_in_unit_tests [EXTRACTED 1.00]
- **OpenAPI Document and C# Client Generation Flow** — docs_guides_openapi_1_openapi_host_program, docs_guides_openapi_1_nswag_config, docs_guides_openapi_1_nswag_local_tool, docs_guides_openapi_1_generation_pipeline, src_eventshub_openapi_readme_generated_rpc_clients [EXTRACTED 1.00]
- **Mutation requests reusing Get-404 test script** — tests_eventshub_integrationtests_events_events___create__200, tests_eventshub_integrationtests_events_events___edit___204, tests_eventshub_integrationtests_events_events___delete__200, tests_eventshub_integrationtests_events_events___get___404 [INFERRED 0.85]
- **CQRS Distributed Reliability Patterns** — docs_fundamentals_cqrsfundamentals_eventual_consistency, docs_fundamentals_cqrsfundamentals_outbox_pattern, docs_fundamentals_cqrsfundamentals_sagas, docs_fundamentals_cqrsfundamentals_idempotency_concurrency, docs_fundamentals_cqrsfundamentals_projections [INFERRED 0.85]
- **Social/brand link icons (Vite template)** — web_eventshub_public_icons_bluesky_icon, web_eventshub_public_icons_discord_icon, web_eventshub_public_icons_github_icon, web_eventshub_public_icons_x_icon [INFERRED 0.85]

## Communities (63 total, 22 thin omitted)

### Community 0 - "Event"
Cohesion: 0.06
Nodes (26): EventsController, Command, Event, Command, Id, Command, Event, Handler (+18 more)

### Community 1 - "Task"
Cohesion: 0.06
Nodes (21): EventsHub.OpenApi.Client, ApiException, ApiException, Result, Headers, Response, StatusCode, DateFormatConverter (+13 more)

### Community 2 - "EventsHub Architecture Document"
Cohesion: 0.26
Nodes (12): EventsHub Architecture Document, API Startup (migrate + seed + CORS), Brittle Bruno Test with Fixed Event Id, Request Flow: Loading the Event List, EventsHub Root README, EventsHub.Api README, CORS Origins localhost:3000, EventsHub.Domain README (+4 more)

### Community 3 - "AppDbContext"
Cohesion: 0.05
Nodes (24): EventsHub.Domain, EventsHub.UnitTests.Core, EventsHub.UnitTests.Events, EventsHub.Application.Events.Queries, EventsHub.Api.Controllers, EventsHub.Application.Events.Commands, EventsHub.UnitTests, EventsHub.Persistence (+16 more)

### Community 4 - "EventsHub.UnitTests.csproj"
Cohesion: 0.08
Nodes (25): coverlet.collector (6.0.4), MediatR (14.2.0), Microsoft.AspNetCore.Mvc.NewtonsoftJson (10.0.11), Microsoft.AspNetCore.OpenApi (10.0.11), Microsoft.EntityFrameworkCore.Design (10.0.11), Microsoft.EntityFrameworkCore.Sqlite (10.0.11), Microsoft.NET.Test.Sdk (17.14.0), Moq (4.20.72) (+17 more)

### Community 5 - "EventsHub.IntegrationTests README"
Cohesion: 0.29
Nodes (11): baseUrl (local environment), Events - Create- 200 (Bruno request), Events - Delete- 200 (Bruno request), Events - Edit - 204 (Bruno request), Events - Get - 200 (Bruno request), Events - Get - 404 (Bruno request), Events - List - 200 (Bruno request), EventsHub.IntegrationTests OpenCollection (+3 more)

### Community 6 - "WeatherForecast"
Cohesion: 0.25
Nodes (6): EventsHub.Api, WeatherForecast, Date, Summary, TemperatureC, TemperatureF

### Community 7 - "The Dependency Rule"
Cohesion: 0.10
Nodes (21): Clean Architecture Fundamentals Guide, Architecture Tests (NetArchTest/ArchUnitNET), Clean Architecture (Robert C. Martin), Dependency Injection and Composition Root, Hexagonal Architecture (Ports & Adapters), IEventRepository (example abstraction), Onion Architecture, Repository and Unit of Work (+13 more)

### Community 8 - "compilerOptions"
Cohesion: 0.10
Nodes (19): compilerOptions, allowArbitraryExtensions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection (+11 more)

### Community 9 - "20260907164228_InitialCreate.Designer.cs"
Cohesion: 0.16
Nodes (3): EventsHub.Persistence.Migrations, InitialCreate, AppDbContextModelSnapshot

### Community 10 - "devDependencies"
Cohesion: 0.11
Nodes (18): devDependencies, @babel/core, babel-plugin-react-compiler, eslint, @eslint/js, eslint-plugin-react-hooks, eslint-plugin-react-refresh, globals (+10 more)

### Community 11 - "Event"
Cohesion: 0.12
Nodes (16): Event, Category, City, Date, Description, Id, IsCancelled, Latitude (+8 more)

### Community 12 - "compilerOptions"
Cohesion: 0.12
Nodes (16): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, noEmit, noFallthroughCasesInSwitch (+8 more)

### Community 13 - "EventsHub/package.json"
Cohesion: 0.13
Nodes (14): @babel/core, babel-plugin-react-compiler, @emotion/react, @emotion/styled, @mui/icons-material, @types/babel__core, @types/node, @types/react (+6 more)

### Community 14 - "Software Testing Fundamentals Guide"
Cohesion: 0.28
Nodes (9): Test Naming Method_WhenCondition_ExpectedResult, Integration Testing with WebApplicationFactory, Testing with DbContext (InMemory vs SQLite vs real DB), Software Testing Fundamentals Guide, Arrange-Act-Assert Pattern, MethodUnderTest_Scenario_ExpectedResult Naming, Test-Driven Development (Red-Green-Refactor), Test Doubles (dummy, stub, fake, mock, spy) (+1 more)

### Community 15 - "CQRS (Greg Young)"
Cohesion: 0.17
Nodes (11): CQRS: Commands vs Queries (Clean Arch topic), Validation with FluentValidation, MediatR and Pipeline Behaviors, Feature-based Folders (Vertical Slices), CQRS Fundamentals Guide, Commands, Queries, and Events, CQRS (Greg Young), CQRS in Web APIs (HTTP verb mapping) (+3 more)

### Community 16 - "https"
Cohesion: 0.20
Nodes (9): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, profiles, https (+1 more)

### Community 17 - "EventsHub.OpenApi"
Cohesion: 0.22
Nodes (8): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, environmentVariables, launchBrowser, profiles, EventsHub.OpenApi, $schema

### Community 18 - "main.tsx"
Cohesion: 0.32
Nodes (5): @fontsource/roboto, @mui/material, react, react-dom, App()

### Community 19 - "dependencies"
Cohesion: 0.25
Nodes (8): dependencies, @emotion/react, @emotion/styled, @fontsource/roboto, @mui/icons-material, @mui/material, react, react-dom

### Community 20 - "icons.svg (SVG sprite sheet)"
Cohesion: 0.29
Nodes (8): icons.svg (SVG sprite sheet), bluesky-clip clipPath, bluesky-icon, discord-icon, documentation-icon, github-icon, social-icon, x-icon

### Community 21 - "eslint.config.js"
Cohesion: 0.29
Nodes (6): eslint, @eslint/js, eslint-plugin-react-hooks, eslint-plugin-react-refresh, globals, typescript-eslint

### Community 22 - "vite.config.ts"
Cohesion: 0.40
Nodes (4): @rolldown/plugin-babel, vite, vite-plugin-mkcert, @vitejs/plugin-react

### Community 23 - "scripts"
Cohesion: 0.40
Nodes (5): scripts, build, dev, lint, preview

### Community 24 - "axios"
Cohesion: 0.50
Nodes (3): axios, dependencies, axios

### Community 25 - "Projections and Read Model Rebuilding"
Cohesion: 0.67
Nodes (3): Event Sourcing, Eventual Consistency, Projections and Read Model Rebuilding

### Community 26 - "Vite Logo (SVG)"
Cohesion: 0.67
Nodes (3): Prefers-Color-Scheme Dark Mode Styling, Vite Logo (SVG), Vite Build Tool

### Community 36 - "Profile"
Cohesion: 0.09
Nodes (24): 1. Live in `EventsHub.Application/Core`, no new project, 2. Public surface mirrors AutoMapper's subset in use, 3. Convention is member-name equality, resolved once at startup, 4. Immutable registry shared as a singleton, 5. Errors, 6. Extension point for future profiles, Context, Decisions (+16 more)

### Community 37 - "MapperTests"
Cohesion: 0.18
Nodes (12): DuplicatePersonProfile, MapperTests, Person, Age, Name, OnlyInSource, PersonCopy, Age (+4 more)

### Community 38 - "ADDED Requirements"
Cohesion: 0.10
Nodes (19): ADDED Requirements, Purpose, Requirement: Incompatible members fail at startup, Requirement: Mapping is defined by profiles, Requirement: Mapping onto an existing instance, Requirement: Mapping to a new instance, Requirement: Null handling, Requirement: Profiles are discovered from assemblies (+11 more)

### Community 39 - "MapperRegistry"
Cohesion: 0.23
Nodes (3): MapperRegistry, TypeMap, Profile

### Community 40 - "openspec-explore/SKILL.md"
Cohesion: 0.17
Nodes (11): Check for context, Ending Discovery, Guardrails, Handling Different Entry Points, OpenSpec Awareness, Planning a Change, The Stance, What You Don't Have To Do (+3 more)

### Community 41 - "Layered by Project, Not by Behavior"
Cohesion: 0.18
Nodes (9): Activity vs Event Model Divergence (lat/long types), Anemic Domain / Leaky Abstractions / Over-Engineering, API Contracts and OpenAPI, Mapping and DTO Boundaries (AutoMapper/Mapster/manual), Rich Domain Model / Aggregates, Events API Endpoints (/api/v1/events, /{id}, weatherforecast), EventsHub.Application README, Intended Application Layer Contents (use cases, DTOs, validation, interfaces) (+1 more)

### Community 42 - "IMapper"
Cohesion: 0.29
Nodes (8): 1. Mapper core, 2. Switch call sites, 3. Remove AutoMapper and document, Tasks, Workflow follow-up, IMapper, Mapper, Handler

### Community 43 - "explore.md"
Cohesion: 0.18
Nodes (10): Check for context, Ending Discovery, Guardrails, OpenSpec Awareness, Planning a Change, The Stance, What You Don't Have To Do, What You Might Do (+2 more)

### Community 44 - "Graphify (graphifyy knowledge graph tool)"
Cohesion: 0.22
Nodes (7): .claude/CLAUDE.md (graphify skill pointer), architecture-docs Skill, Install Graphify + OpenSpec Guide, Graphify (graphifyy knowledge graph tool), Graphify Git Hooks (post-commit/post-checkout rebuild), .graphifyignore Excluded Paths, OpenSpec (spec-driven development workflow)

### Community 45 - "Current State and Known Inconsistencies"
Cohesion: 0.22
Nodes (8): Generated Client Lands in src/src (nswag relative path), Current State and Known Inconsistencies, OpenAPI Host Does Not Register AppDbContext, Shared AppDbContext Test State, FIRST Principles for Good Tests, Flaky Tests and Shared Mutable State, OpenApi Host Program.cs (AddOpenApiDocument + ApplicationPart), Adding an Endpoint Procedure

### Community 46 - "OpenAPI Manual Setup Guide (OpenApi 1.md)"
Cohesion: 0.46
Nodes (8): OpenAPI and Client Generation Pipeline, OpenAPI Manual Setup Guide (OpenApi 1.md), Generation Pipeline (run host -> fetch JSON -> nswag run), EventsHub.nswag Codegen Config, NSwag Local Dotnet Tool (nswag.consolecore 14.7.1), EventsHub.OpenApi README, Generated RpcClients (EventsRpcClient, WeatherForecastRpcClient), Regenerating Document and Client

### Community 47 - "CLAUDE.md (project guidance)"
Cohesion: 0.29
Nodes (6): CLAUDE.md (project guidance), Commit Convention feat(<branch>) - <description>, Git in Practice Guide, Branch Naming Prefixes (feature/fix/hotfix/chore), Merge, Cherry-pick and Squash, Pull Requests (gh CLI)

### Community 48 - "EventsHub Web README"
Cohesion: 0.29
Nodes (4): ASP.NET Core dev certificate trust, index.html (Events Hub), EventsHub Web README, vite-plugin-mkcert HTTPS

### Community 50 - "RequiredPersonCopy"
Cohesion: 0.67
Nodes (3): RequiredPersonCopy, Age, Name

## Ambiguous Edges - Review These
- `Events - Create- 200 (Bruno request)` → `Events - Get - 404 (Bruno request)`  [AMBIGUOUS]
  tests/EventsHub.IntegrationTests/Events/Events - Create- 200.yml · relation: semantically_similar_to

## Knowledge Gaps
- **140 isolated node(s):** `Mediator`, `net10.0`, `Microsoft.AspNetCore.OpenApi (10.0.11)`, `Microsoft.EntityFrameworkCore.Design (10.0.11)`, `Microsoft.NET.Sdk.Web` (+135 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 314 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **22 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Event` connect `Event` to `Task`, `AppDbContext`, `Profile`, `20260907164228_InitialCreate.Designer.cs`?**
  _High betweenness centrality (0.035) - this node is a cross-community bridge._
- **Are the 2 inferred relationships involving `EventsHub Architecture Document` (e.g. with `CLAUDE.md (project guidance)` and `architecture-docs Skill`) actually correct?**
  _`EventsHub Architecture Document` has 2 INFERRED edges - model-reasoned connections that need verification._
- **What connects `Mediator`, `net10.0`, `Microsoft.AspNetCore.OpenApi (10.0.11)` to the rest of the system?**
  _140 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Event` be split into smaller, more focused modules?**
  _Cohesion score 0.058001397624039136 - nodes in this community are weakly interconnected._
- **Why does `Profile` connect `Profile` to `Event`, `AppDbContext`, `MapperTests`, `MapperRegistry`, `IMapper`?**
  _High betweenness centrality (0.023) - this node is a cross-community bridge._
- **Should `Task` be split into smaller, more focused modules?**
  _Cohesion score 0.06376811594202898 - nodes in this community are weakly interconnected._

### Low-confidence Hints
_AMBIGUOUS edges — the extractor was unsure. Verify before acting on these._

- **What is the exact relationship between `Events - Create- 200 (Bruno request)` and `Events - Get - 404 (Bruno request)`?**
  _Edge tagged AMBIGUOUS (relation: semantically_similar_to) - confidence is low._