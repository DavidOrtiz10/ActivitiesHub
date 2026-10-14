# Graph Report - EventsHub  (2026-10-14)

## Corpus Check
- 91 files · ~73,217 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 10 file(s) not represented in the graph (top: (none) 7, .css 2, .nswag 1)

## Summary
- 721 nodes · 1120 edges · 65 communities (45 shown, 20 thin omitted)
- Extraction: 90% EXTRACTED · 10% INFERRED · 0% AMBIGUOUS · INFERRED: 114 edges (avg confidence: 0.87)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `df8fc18e`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- EventsControllerTests
- Task
- EventsHub Architecture Document
- EventsHub.Persistence
- EventsHub.UnitTests.csproj
- EventsHub.IntegrationTests README
- WeatherForecast
- The Dependency Rule
- compilerOptions
- ADDED Requirements
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
- Requirements
- vite.config.ts
- scripts
- .GetEventsAsync
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
- AppDbContext
- MapperRegistry
- openspec-explore/SKILL.md
- Layered by Project, Not by Behavior
- IMapper
- explore.md
- CLAUDE.md (project guidance)
- Event
- OpenAPI Manual Setup Guide (OpenApi 1.md)
- EventsHub.Application.Core
- EventsHub Web README
- EventsHub Root README
- Handler
- Handler
- IRequest
- Proposal
- Command

## God Nodes (most connected - your core abstractions)
1. `Event` - 24 edges
2. `EventsHub Architecture Document` - 21 edges
3. `MapperTests` - 20 edges
4. `EventsRpcClient` - 20 edges
5. `Profile` - 19 edges
6. `WeatherForecastRpcClient` - 19 edges
7. `compilerOptions` - 18 edges
8. `compilerOptions` - 15 edges
9. `IMapper` - 13 edges
10. `PersonProfile` - 13 edges

## Surprising Connections (you probably didn't know these)
- `1. Live in `EventsHub.Application/Core`, no new project` --references--> `MappingProfiles`  [INFERRED]
  openspec/changes/archive/2026-10-14-replace-automapper-custom-mapper/design.md → src/EventsHub.Application/Core/MappingProfiles.cs
- `Context` --references--> `Handler`  [INFERRED]
  openspec/changes/archive/2026-10-14-replace-automapper-custom-mapper/design.md → src/EventsHub.Application/Events/Commands/EditEvent.cs
- `Goals / Non-Goals` --references--> `Profile`  [INFERRED]
  openspec/changes/archive/2026-10-14-replace-automapper-custom-mapper/design.md → src/EventsHub.Application/Core/Profile.cs
- `2. Public surface mirrors AutoMapper's subset in use` --references--> `Profile`  [INFERRED]
  openspec/changes/archive/2026-10-14-replace-automapper-custom-mapper/design.md → src/EventsHub.Application/Core/Profile.cs
- `2. Public surface mirrors AutoMapper's subset in use` --references--> `EditEvent`  [INFERRED]
  openspec/changes/archive/2026-10-14-replace-automapper-custom-mapper/design.md → src/EventsHub.Application/Events/Commands/EditEvent.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Events CRUD integration requests against /Events** — tests_eventshub_integrationtests_events_events___list___200, tests_eventshub_integrationtests_events_events___get___200, tests_eventshub_integrationtests_events_events___get___404, tests_eventshub_integrationtests_events_events___create__200, tests_eventshub_integrationtests_events_events___edit___204, tests_eventshub_integrationtests_events_events___delete__200 [EXTRACTED 1.00]
- **EventsHub Key Architectural Decisions** — docs_architecture_layered_by_project, docs_architecture_sqlite_file_database, docs_architecture_separate_documentation_host, docs_architecture_url_based_versioning, docs_architecture_real_database_in_unit_tests [EXTRACTED 1.00]
- **OpenAPI Document and C# Client Generation Flow** — docs_guides_openapi_1_openapi_host_program, docs_guides_openapi_1_nswag_config, docs_guides_openapi_1_nswag_local_tool, docs_guides_openapi_1_generation_pipeline, src_eventshub_openapi_readme_generated_rpc_clients [EXTRACTED 1.00]
- **Mutation requests reusing Get-404 test script** — tests_eventshub_integrationtests_events_events___create__200, tests_eventshub_integrationtests_events_events___edit___204, tests_eventshub_integrationtests_events_events___delete__200, tests_eventshub_integrationtests_events_events___get___404 [INFERRED 0.85]
- **CQRS Distributed Reliability Patterns** — docs_fundamentals_cqrsfundamentals_eventual_consistency, docs_fundamentals_cqrsfundamentals_outbox_pattern, docs_fundamentals_cqrsfundamentals_sagas, docs_fundamentals_cqrsfundamentals_idempotency_concurrency, docs_fundamentals_cqrsfundamentals_projections [INFERRED 0.85]
- **Social/brand link icons (Vite template)** — web_eventshub_public_icons_bluesky_icon, web_eventshub_public_icons_discord_icon, web_eventshub_public_icons_github_icon, web_eventshub_public_icons_x_icon [INFERRED 0.85]

## Communities (65 total, 20 thin omitted)

### Community 0 - "EventsControllerTests"
Cohesion: 0.13
Nodes (4): Command, Event, EventsControllerTests, EditEventTests

### Community 1 - "Task"
Cohesion: 0.08
Nodes (17): EventsHub.OpenApi.Client, ApiException, Result, DateFormatConverter, EventsRpcClient, BaseUrl, JsonSerializerSettings, ReadResponseAsString (+9 more)

### Community 2 - "EventsHub Architecture Document"
Cohesion: 0.20
Nodes (14): EventsHub Architecture Document, API Startup (migrate + seed + CORS), Brittle Bruno Test with Fixed Event Id, Activity vs Event Model Divergence (lat/long types), Request Flow: Loading the Event List, EventsHub.Api README, CORS Origins localhost:3000, Events API Endpoints (/api/v1/events, /{id}, weatherforecast) (+6 more)

### Community 3 - "EventsHub.Persistence"
Cohesion: 0.06
Nodes (14): EventsHub.Domain, EventsHub.Persistence.Migrations, EventsHub.UnitTests.Events, EventsHub.Application.Events.Queries, EventsHub.Api.Controllers, EventsHub.Application.Events.Commands, EventsHub.UnitTests, EventsHub.Persistence (+6 more)

### Community 4 - "EventsHub.UnitTests.csproj"
Cohesion: 0.07
Nodes (25): net10.0, coverlet.collector (6.0.4), MediatR (14.2.0), Microsoft.AspNetCore.Mvc.NewtonsoftJson (10.0.11), Microsoft.AspNetCore.OpenApi (10.0.11), Microsoft.EntityFrameworkCore.Design (10.0.11), Microsoft.EntityFrameworkCore.Sqlite (10.0.11), Microsoft.NET.Test.Sdk (17.14.0) (+17 more)

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

### Community 9 - "ADDED Requirements"
Cohesion: 0.10
Nodes (19): ADDED Requirements, Purpose, Requirement: Incompatible members fail at startup, Requirement: Mapping is defined by profiles, Requirement: Mapping onto an existing instance, Requirement: Mapping to a new instance, Requirement: Null handling, Requirement: Profiles are discovered from assemblies (+11 more)

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
Cohesion: 0.11
Nodes (20): @babel/core, babel-plugin-react-compiler, @emotion/react, @emotion/styled, eslint, @eslint/js, eslint-plugin-react-hooks, eslint-plugin-react-refresh (+12 more)

### Community 14 - "Software Testing Fundamentals Guide"
Cohesion: 0.19
Nodes (12): Test Naming Method_WhenCondition_ExpectedResult, Shared AppDbContext Test State, Integration Testing with WebApplicationFactory, Testing with DbContext (InMemory vs SQLite vs real DB), Software Testing Fundamentals Guide, Arrange-Act-Assert Pattern, FIRST Principles for Good Tests, MethodUnderTest_Scenario_ExpectedResult Naming (+4 more)

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
Cohesion: 0.20
Nodes (8): axios, @fontsource/roboto, @mui/material, react, react-dom, App(), dependencies, axios

### Community 19 - "dependencies"
Cohesion: 0.25
Nodes (8): dependencies, @emotion/react, @emotion/styled, @fontsource/roboto, @mui/icons-material, @mui/material, react, react-dom

### Community 20 - "icons.svg (SVG sprite sheet)"
Cohesion: 0.29
Nodes (8): icons.svg (SVG sprite sheet), bluesky-clip clipPath, bluesky-icon, discord-icon, documentation-icon, github-icon, social-icon, x-icon

### Community 21 - "Requirements"
Cohesion: 0.10
Nodes (19): object-mapping Specification, Purpose, Requirement: Incompatible members fail at startup, Requirement: Mapping is defined by profiles, Requirement: Mapping onto an existing instance, Requirement: Mapping to a new instance, Requirement: Null handling, Requirement: Profiles are discovered from assemblies (+11 more)

### Community 22 - "vite.config.ts"
Cohesion: 0.40
Nodes (4): @rolldown/plugin-babel, vite, vite-plugin-mkcert, @vitejs/plugin-react

### Community 23 - "scripts"
Cohesion: 0.40
Nodes (5): scripts, build, dev, lint, preview

### Community 25 - "Projections and Read Model Rebuilding"
Cohesion: 0.67
Nodes (3): Event Sourcing, Eventual Consistency, Projections and Read Model Rebuilding

### Community 26 - "Vite Logo (SVG)"
Cohesion: 0.67
Nodes (3): Prefers-Color-Scheme Dark Mode Styling, Vite Logo (SVG), Vite Build Tool

### Community 36 - "Profile"
Cohesion: 0.31
Nodes (8): What Changes, 2. Switch call sites, 3. Remove AutoMapper and document, Tasks, Workflow follow-up, MappingProfiles, Profile, Maps

### Community 37 - "MapperTests"
Cohesion: 0.10
Nodes (21): 6. Extension point for future profiles, Coordinates, Latitude, DuplicatePersonProfile, IncompatibleProfile, MapperTests, NumericCoordinates, Latitude (+13 more)

### Community 38 - "AppDbContext"
Cohesion: 0.18
Nodes (5): AppDbContext, Events, DbInitializer, GlobalTestSetup, AppDbContext

### Community 39 - "MapperRegistry"
Cohesion: 0.15
Nodes (7): MapperRegistry, TypeMap, Profile, ApiException, Headers, Response, StatusCode

### Community 40 - "openspec-explore/SKILL.md"
Cohesion: 0.17
Nodes (11): Check for context, Ending Discovery, Guardrails, Handling Different Entry Points, OpenSpec Awareness, Planning a Change, The Stance, What You Don't Have To Do (+3 more)

### Community 41 - "Layered by Project, Not by Behavior"
Cohesion: 0.29
Nodes (6): Anemic Domain / Leaky Abstractions / Over-Engineering, API Contracts and OpenAPI, Mapping and DTO Boundaries (AutoMapper/Mapster/manual), Rich Domain Model / Aggregates, EventsHub.Application README, Intended Application Layer Contents (use cases, DTOs, validation, interfaces)

### Community 42 - "IMapper"
Cohesion: 0.22
Nodes (8): 1. Live in `EventsHub.Application/Core`, no new project, 2. Public surface mirrors AutoMapper's subset in use, 3. Convention is member-name equality, resolved once at startup, 4. Immutable registry shared as a singleton, 5. Errors, Decisions, IMapper, Mapper

### Community 43 - "explore.md"
Cohesion: 0.18
Nodes (10): Check for context, Ending Discovery, Guardrails, OpenSpec Awareness, Planning a Change, The Stance, What You Don't Have To Do, What You Might Do (+2 more)

### Community 44 - "CLAUDE.md (project guidance)"
Cohesion: 0.15
Nodes (10): .claude/CLAUDE.md (graphify skill pointer), architecture-docs Skill, CLAUDE.md (project guidance), Commit Convention feat(<branch>) - <description>, Branch Naming Prefixes (feature/fix/hotfix/chore), Install Graphify + OpenSpec Guide, Graphify (graphifyy knowledge graph tool), Graphify Git Hooks (post-commit/post-checkout rebuild) (+2 more)

### Community 45 - "Event"
Cohesion: 0.17
Nodes (11): Event, Category, City, Date, Description, Id, IsCancelled, Latitude (+3 more)

### Community 46 - "OpenAPI Manual Setup Guide (OpenApi 1.md)"
Cohesion: 0.23
Nodes (13): Generated Client Lands in src/src (nswag relative path), Current State and Known Inconsistencies, OpenAPI and Client Generation Pipeline, OpenAPI Host Does Not Register AppDbContext, OpenAPI Manual Setup Guide (OpenApi 1.md), Generation Pipeline (run host -> fetch JSON -> nswag run), EventsHub.nswag Codegen Config, NSwag Local Dotnet Tool (nswag.consolecore 14.7.1) (+5 more)

### Community 47 - "EventsHub.Application.Core"
Cohesion: 0.24
Nodes (3): EventsHub.UnitTests.Core, EventsHub.Application.Core, MapperServiceCollectionExtensions

### Community 48 - "EventsHub Web README"
Cohesion: 0.29
Nodes (4): ASP.NET Core dev certificate trust, index.html (Events Hub), EventsHub Web README, vite-plugin-mkcert HTTPS

### Community 49 - "EventsHub Root README"
Cohesion: 0.22
Nodes (5): Git in Practice Guide, Merge, Cherry-pick and Squash, Pull Requests (gh CLI), EventsHub Root README, EventsHub.UnitTests README

### Community 50 - "Handler"
Cohesion: 0.31
Nodes (7): Context, Design, Goals / Non-Goals, Migration Plan, Risks / Trade-offs, EditEvent, Handler

### Community 51 - "Handler"
Cohesion: 0.33
Nodes (4): Handler, GetEventsList, Handler, Query

### Community 52 - "IRequest"
Cohesion: 0.22
Nodes (8): Command, Event, Command, Id, DeleteEvent, GetEventQueries, Query, Id

### Community 63 - "Proposal"
Cohesion: 0.29
Nodes (6): Capabilities, Impact, Modified Capabilities, New Capabilities, Proposal, Why

### Community 64 - "Command"
Cohesion: 0.47
Nodes (3): CreateEvent, Handler, Handler

## Ambiguous Edges - Review These
- `Events - Create- 200 (Bruno request)` → `Events - Get - 404 (Bruno request)`  [AMBIGUOUS]
  tests/EventsHub.IntegrationTests/Events/Events - Create- 200.yml · relation: semantically_similar_to

## Knowledge Gaps
- **140 isolated node(s):** `Profile`, `Maps`, `Event`, `net10.0`, `MediatR (14.2.0)` (+135 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 329 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **20 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Event` connect `Event` to `EventsHub.Persistence`, `AppDbContext`, `Handler`, `IRequest`, `.GetEventsAsync`?**
  _High betweenness centrality (0.029) - this node is a cross-community bridge._
- **Are the 2 inferred relationships involving `EventsHub Architecture Document` (e.g. with `CLAUDE.md (project guidance)` and `architecture-docs Skill`) actually correct?**
  _`EventsHub Architecture Document` has 2 INFERRED edges - model-reasoned connections that need verification._
- **What connects `Profile`, `Maps`, `Event` to the rest of the system?**
  _140 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `EventsControllerTests` be split into smaller, more focused modules?**
  _Cohesion score 0.13071895424836602 - nodes in this community are weakly interconnected._
- **Why does `Profile` connect `Profile` to `MapperTests`, `MapperRegistry`, `IMapper`, `EventsHub.Application.Core`, `Handler`, `Handler`, `.GetEventsAsync`, `Proposal`?**
  _High betweenness centrality (0.027) - this node is a cross-community bridge._
- **Should `Task` be split into smaller, more focused modules?**
  _Cohesion score 0.07853107344632769 - nodes in this community are weakly interconnected._

### Low-confidence Hints
_AMBIGUOUS edges — the extractor was unsure. Verify before acting on these._

- **What is the exact relationship between `Events - Create- 200 (Bruno request)` and `Events - Get - 404 (Bruno request)`?**
  _Edge tagged AMBIGUOUS (relation: semantically_similar_to) - confidence is low._