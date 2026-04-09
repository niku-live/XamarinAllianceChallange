# 04-rewrite-startup: Replace OWIN startup with ASP.NET Core Program.cs

Replace `Startup.cs` + `App_Start/Startup.MobileApp.cs` (OWIN entry point) with a modern ASP.NET Core `Program.cs` using `WebApplication.CreateBuilder()`.

The new entry point must configure:
- **Routing**: `app.MapControllers()` with attribute routing
- **OData**: Register `Microsoft.AspNetCore.OData` services and route conventions
- **Authentication**: JWT Bearer middleware (`AddAuthentication().AddJwtBearer(...)`) using options loaded from `IConfiguration`, replacing the `UseAppServiceAuthentication` OWIN middleware
- **EF Core**: Register `XamarinBackendContext` via `services.AddDbContext<>()` with SQL Server provider and connection string from `IConfiguration`
- **Dependency Injection**: All services registered in `builder.Services`

Remove `Web.config` and `Web.Debug.config` / `Web.Release.config`. Create `appsettings.json` with the connection string (`MS_TableConnectionString`) and app settings (`SigningKey`, `ValidAudience`, `ValidIssuer`).

**Done when**: `Program.cs` compiles; `Startup.cs` and `App_Start/Startup.MobileApp.cs` removed; `appsettings.json` contains all migrated settings from `Web.config`; `Web.config` removed.
