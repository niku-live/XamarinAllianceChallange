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

---

## Research Findings

### OWIN Startup Analysis

**`Startup.cs`** — Just entry point, calls `ConfigureMobileApp(app)`
**`App_Start/Startup.MobileApp.cs`** — Contains:
- `HttpConfiguration` + `MobileAppConfiguration().UseDefaultConfiguration()` → replace with `AddControllers().AddOData(...)`
- `Database.SetInitializer<XamarinBackendContext>(null)` → remove (EF Core doesn't use this API)
- `UseAppServiceAuthentication` with `SigningKey`, `ValidAudience`, `ValidIssuer` → replace with `AddAuthentication().AddJwtBearer(...)`
- `app.UseWebApi(config)` → replace with `app.MapControllers()`

### Settings to Migrate from Web.config
| Config | Source | Target |
|--------|--------|--------|
| Connection string `MS_TableConnectionString` | `<connectionStrings>` | `appsettings.json` → `ConnectionStrings:MS_TableConnectionString` |
| `SigningKey` | `<appSettings>` | `appsettings.json` → `JwtSettings:SigningKey` |
| `ValidAudience` | `<appSettings>` | `appsettings.json` → `JwtSettings:ValidAudience` |
| `ValidIssuer` | `<appSettings>` | `appsettings.json` → `JwtSettings:ValidIssuer` |

### Connection String Change
Original used `|DataDirectory|` (ASP.NET-specific token). Replace with LocalDB named database pattern:
`Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=XamarinAllianceDB;Integrated Security=True;MultipleActiveResultSets=True`

### OData EDM Model
Entities: `Character`, `Movie`, `Weapon` — all in `XamarinBackendService.DataObjects`.
Route prefix: `"tables"` (matches Azure Mobile Server convention used by Xamarin client).

### Files Affected
- **Create**: `Program.cs`, `appsettings.json`
- **Delete**: `Startup.cs`, `App_Start/Startup.MobileApp.cs`, `Web.config`
- Note: `Web.Debug.config` and `Web.Release.config` already deleted in Task 02

