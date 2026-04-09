# Task 04 — Rewrite Startup Progress Details

## What Changed

### Files Created
- **`Program.cs`** — New ASP.NET Core entry point, replacing `Startup.cs` + `App_Start/Startup.MobileApp.cs`
- **`appsettings.json`** — Settings migrated from `Web.config`

### Files Deleted
- `Startup.cs` — OWIN startup entry point (`[assembly: OwinStartup]`)
- `App_Start/Startup.MobileApp.cs` — OWIN mobile app configuration
- `App_Start/` folder — now empty, deleted
- `Web.config` — Replaced by `appsettings.json`

### Package Added
- `Microsoft.AspNetCore.Authentication.JwtBearer` 10.0.5 — required explicitly for net10.0 (not bundled via `Microsoft.AspNetCore.App` in this project setup)

### Startup Migration Summary

| Old (OWIN) | New (ASP.NET Core) |
|------------|-------------------|
| `[assembly: OwinStartup]` | Pipeline is `Program.cs` |
| `HttpConfiguration` | Built into `AddControllers()` |
| `MobileAppConfiguration().UseDefaultConfiguration()` | `AddControllers().AddOData(...)` |
| `Database.SetInitializer<XamarinBackendContext>(null)` | Removed — EF Core has no `SetInitializer` API |
| `UseAppServiceAuthentication` (OWIN JWT middleware) | `AddAuthentication().AddJwtBearer(...)` |
| `app.UseWebApi(config)` | `app.MapControllers()` |
| `ConfigurationManager.AppSettings["SigningKey"]` | `builder.Configuration["JwtSettings:SigningKey"]` |
| `<connectionStrings>` in Web.config | `ConnectionStrings:MS_TableConnectionString` in appsettings.json |

### Settings Migration

`Web.config` → `appsettings.json`:
- `MS_TableConnectionString`: changed `|DataDirectory|` to LocalDB named catalog connection
- `SigningKey` → `JwtSettings:SigningKey`
- `ValidAudience` → `JwtSettings:ValidAudience`
- `ValidIssuer` → `JwtSettings:ValidIssuer`

## Build Status

`dotnet restore` → **Clean** (0 warnings)

`Program.cs` has **0 compilation errors**. Full project build fails due to source files that still reference removed types (`EntityData`, `TableController<T>`, `System.Web.Http.*`, `System.Data.Entity.*`) — these are all resolved in Tasks 05 and 06.
