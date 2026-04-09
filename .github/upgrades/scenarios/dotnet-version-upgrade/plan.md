# .NET Version Upgrade Plan — XamarinBackendService

## Overview

**Target**: Migrate XamarinBackendService from ASP.NET Framework (net4.6) to ASP.NET Core (net10.0)
**Scope**: Single project, ~463 LOC, 3 controllers, 1 DbContext, 23 packages (18 incompatible)

### Selected Strategy
**All-at-Once** — Single project upgraded in one atomic pass.
**Rationale**: 1 project, no dependency graph to manage. .NET Framework migration rules fix the strategy.

---

## Tasks

### 01-prerequisites: Verify SDK and toolchain

Confirm the .NET 10 SDK is correctly installed and accessible, verify the `dotnet` CLI can target `net10.0`, and establish a clean baseline by documenting the current state before any changes.

Check for a `global.json` file at the solution root that might pin an older SDK version and update it if needed. Record the current package restore state (packages.config + HintPath references) for reference during conversion.

**Done when**: `dotnet --version` reports a 10.x SDK; no `global.json` conflicts exist; baseline documented in task.md.

---

### 02-sdk-style-conversion: Convert project to SDK-style format

Convert `XamarinBackendService.csproj` from the legacy MSBuild format (ToolsVersion="12.0", explicit `<Compile>` includes, `<Import>` of WebApplication targets) to SDK-style format while keeping the target framework at net4.6 for now.

This is a structural-only change — no TFM changes, no package changes yet. Remove:
- Explicit `<Compile Include="...">` entries (SDK-style uses globbing)
- Legacy MSBuild imports (`Microsoft.CSharp.targets`, `Microsoft.WebApplication.targets`)
- `packages.config` (replaced by `<PackageReference>` items added in the next task)
- `AssemblyInfo.cs` attributes that SDK auto-generates (`AssemblyVersion`, `AssemblyTitle`, etc.)
- Web.Debug.config / Web.Release.config XDT transforms (no equivalent in ASP.NET Core)

The project must restore and build (even if only partially — legacy references may not resolve) after conversion.

**Done when**: `XamarinBackendService.csproj` is SDK-style with `<Project Sdk="Microsoft.NET.Sdk.Web">`; `packages.config` removed; `AssemblyInfo.cs` cleaned of auto-generated attributes; project file is minimal and readable.

---

### 03-tfm-and-packages: Upgrade to net10.0 and replace all packages

Change the target framework from `net4.6` to `net10.0` and replace all 23 NuGet packages with their ASP.NET Core equivalents, resolving all incompatibilities inline.

Key replacements to research and apply:
- **Azure Mobile Server SDK** (6 packages, all incompatible): Remove entirely. `TableController<T>` and `EntityDomainManager<T>` have no ASP.NET Core equivalent — the controller pattern will be rewritten in the next task.
- **OWIN stack** (`Microsoft.Owin`, `Owin`, `Microsoft.Owin.Host.SystemWeb`, `Microsoft.Owin.Security`, `Microsoft.AspNet.WebApi.Owin`): Remove entirely. ASP.NET Core has its own middleware pipeline — no OWIN needed.
- **ASP.NET WebApi** (`Microsoft.AspNet.WebApi.Core`, `.Client`, `.Owin`, `.Tracing`): Remove. Built into ASP.NET Core.
- **Legacy OData** (`Microsoft.AspNet.WebApi.OData`, `Microsoft.Data.OData`, `Microsoft.Data.Edm`, `System.Spatial`): Remove legacy stack. Add `Microsoft.AspNetCore.OData` (v9.x) for the new OData stack.
- **EntityFramework 6.x**: Remove. Add `Microsoft.EntityFrameworkCore` + `Microsoft.EntityFrameworkCore.SqlServer` (EF Core 9.x). EF 6.1.3 cannot run on .NET Core — migration is mandatory.
- **AutoMapper 5.2.0**: Upgrade to AutoMapper 13.x (breaking changes between v5 and v13 — research mapping profile API changes).
- **Microsoft.WindowsAzure.ConfigurationManager**: Remove. Replace with `Microsoft.Extensions.Configuration` (built into ASP.NET Core).
- **System.IdentityModel.Tokens.Jwt 5.7.0**: Upgrade to `Microsoft.IdentityModel.Tokens` / `System.IdentityModel.Tokens.Jwt` 8.x.
- **Newtonsoft.Json 13.0.1**: Keep (compatible with net10.0, already latest minor).

After package changes: run `dotnet restore`. Build errors expected — resolved in next tasks.

**Done when**: All `<PackageReference>` items target net10.0-compatible versions; `dotnet restore` succeeds with no NU1701 or incompatibility errors; package list reflects only net10.0-compatible packages.

---

### 04-rewrite-startup: Replace OWIN startup with ASP.NET Core Program.cs

Replace `Startup.cs` + `App_Start/Startup.MobileApp.cs` (OWIN entry point) with a modern ASP.NET Core `Program.cs` using `WebApplication.CreateBuilder()`.

The new entry point must configure:
- **Routing**: `app.MapControllers()` with attribute routing
- **OData**: Register `Microsoft.AspNetCore.OData` services and route conventions
- **Authentication**: JWT Bearer middleware (`AddAuthentication().AddJwtBearer(...)`) using options loaded from `IConfiguration`, replacing the `UseAppServiceAuthentication` OWIN middleware
- **EF Core**: Register `XamarinBackendContext` via `services.AddDbContext<>()` with SQL Server provider and connection string from `IConfiguration`
- **Dependency Injection**: All services registered in `builder.Services`

Remove `Web.config` and `Web.Debug.config` / `Web.Release.config`. Create `appsettings.json` with the connection string (`MS_TableConnectionString`) and app settings (`SigningKey`, `ValidAudience`, `ValidIssuer`).

**Done when**: `Program.cs` compiles; `Startup.cs` and `App_Start/Startup.MobileApp.cs` removed; `appsettings.json` contains all migrated settings from `Web.config`; `Web.config` removed.

---

### 05-rewrite-data-layer: Migrate EF context and entities to EF Core

Rewrite `XamarinBackendContext` and the `DataObjects` entity classes to remove the Azure Mobile Server SDK dependency (`EntityData`, `TableColumnAttribute`, `IAzureTableData`).

Changes required:
- `BaseDataObject`: Remove `EntityData` base class. Define own base with equivalent properties: `Id` (string), `CreatedAt` (DateTimeOffset?), `UpdatedAt` (DateTimeOffset?), `Version` (byte[]), `Deleted` (bool). These are the soft-delete/sync columns the Azure Mobile Client SDK expects in a standard REST API.
- `XamarinBackendContext`: Remove `Microsoft.Azure.Mobile.Server.Tables` conventions (`AttributeToColumnAnnotationConvention`, `TableColumnAttribute`). Replace with EF Core `DbContext` using `OnModelCreating` with standard EF Core Fluent API. Update connection string loading to use `IConfiguration` passed via DI.
- Entity classes (`Character`, `Movie`, `Weapon`): Remove `BaseDataObject` and inherit from the new base or use POCO. Verify navigation properties are correct for EF Core.
- Add EF Core migration or configure `EnsureCreated()` for development.

**Done when**: `XamarinBackendContext` compiles with EF Core only; all entities are POCO or use new base class; no references to `Microsoft.Azure.Mobile.Server` remain in the data layer; `dotnet build` succeeds for the data layer.

---

### 06-rewrite-controllers: Replace TableController with ControllerBase

Rewrite all three controllers (`CharacterController`, `MovieController`, `WeaponController`) to replace `TableController<T>` (Azure Mobile Server) with standard ASP.NET Core `ControllerBase`.

Each controller must:
- Inherit from `ControllerBase` with `[ApiController]` and `[Route("tables/[controller]")]` attributes
- Accept `XamarinBackendContext` via constructor injection (DI)
- Implement GET (all), GET (by id) using EF Core LINQ queries
- Apply `[EnableQuery]` from `Microsoft.AspNetCore.OData` for OData query support (replaces `[QueryableExpand]` custom attribute)
- Remove the `QueryableExpandAttribute` helper (replaced by OData's built-in `[EnableQuery]`)

The in-memory test data (`_test` list) currently in `CharacterController` can remain as the default data source or be replaced with EF Core queries — follow what was already partially implemented (the commented-out `Query()` / `Lookup()` calls suggest EF was the intent).

**Done when**: All 3 controllers compile; no references to `Microsoft.Azure.Mobile.Server` remain; each controller has working GET endpoints; `QueryableExpandAttribute` removed; `dotnet build` reports 0 errors and 0 warnings for the project.

---

### 07-final-validation: Full build and smoke test

Validate the complete migration: build the project, run any existing tests, and document the final state.

- Run `dotnet build XamarinBackendService/XamarinBackendService.csproj` — must succeed with 0 errors and 0 warnings
- Run `dotnet run` briefly to verify the application starts without exceptions
- Verify OData endpoint metadata is reachable (`/odata/$metadata` or equivalent)
- Document any deferred recommendations (e.g., enabling nullable reference types, adding integration tests, EF Core migration scripts for production)
- Commit all changes to the `dotnet-version-upgrade` branch

**Done when**: Project builds with 0 errors and 0 warnings; application starts successfully; no references to `Microsoft.Azure.Mobile.Server`, `System.Web`, or `Owin` remain anywhere in the project; commit made on working branch.
