# 03-tfm-and-packages: Upgrade to net10.0 and replace all packages

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

## Research Findings

### Confirmed Package Plan (19 → 7 packages)

**REMOVE (14 packages):**
| Package | Reason |
|---------|--------|
| `Microsoft.Azure.Mobile.Server` 2.0.0 | SDK discontinued; Azure Mobile Server SDK has no .NET Core support |
| `Microsoft.Azure.Mobile.Server.Authentication` 2.0.0 | Same |
| `Microsoft.Azure.Mobile.Server.Entity` 2.0.0 | Same |
| `Microsoft.Azure.Mobile.Server.Home` 2.0.0 | Same |
| `Microsoft.Azure.Mobile.Server.Quickstart` 2.0.0 | Same |
| `Microsoft.Azure.Mobile.Server.Tables` 2.0.0 | Same |
| `Microsoft.AspNet.WebApi.Core` 5.2.3 | Built into ASP.NET Core SDK |
| `Microsoft.AspNet.WebApi.Client` 5.2.3 | Built into ASP.NET Core SDK |
| `Microsoft.AspNet.WebApi.Owin` 5.2.3 | OWIN middleware — not needed |
| `Microsoft.AspNet.WebApi.Tracing` 5.2.3 | Replaced by ASP.NET Core logging |
| `Microsoft.AspNet.WebApi.OData` 5.7.0 | Legacy OData — replaced by Microsoft.AspNetCore.OData |
| `Microsoft.Data.OData` 5.8.4 | Legacy OData dependency |
| `Microsoft.Data.Edm` 5.8.1 | Legacy OData dependency |
| `System.Spatial` 5.8.1 | Legacy OData dependency |
| `EntityFramework` 6.1.3 | EF6 cannot run on .NET Core |
| `Microsoft.WindowsAzure.ConfigurationManager` 3.2.3 | Replaced by Microsoft.Extensions.Configuration (built-in) |
| `System.IdentityModel.Tokens.Jwt` 5.7.0 | Replace with 8.x series |

**KEEP (1 package):**
| Package | Action |
|---------|--------|
| `Newtonsoft.Json` 13.0.1 | Compatible with net10.0; keep at current version |

**ADD (6 packages):**
| Package | Version | Purpose |
|---------|---------|---------|
| `Microsoft.AspNetCore.OData` | 9.2.2 | OData support in ASP.NET Core |
| `Microsoft.EntityFrameworkCore.SqlServer` | 9.0.4 | EF Core + SQL Server provider |
| `Microsoft.EntityFrameworkCore.Tools` | 9.0.4 | EF Core CLI tooling (migrations) |
| `AutoMapper` | 13.0.1 | (upgrade from 5.2.0 — breaking API changes handled in Tasks 05-06) |
| `Microsoft.IdentityModel.Tokens` | 8.9.0 | JWT validation |
| `System.IdentityModel.Tokens.Jwt` | 8.9.0 | JWT token handling |

### AutoMapper 5 → 13 Breaking Change Notes
- Profile-based configuration still works; `Mapper.Initialize()` static API removed → use DI `IMapper`
- `CreateMap<>()` method unchanged in profiles — same syntax
- Actual usage changes handled in Task 06 (controllers)

### EF Core 9.x on net10.0
EF Core 9.x targets net8.0+. On net10.0, NuGet resolves via forward-compatibility — this is fully supported and the standard approach until EF Core 10 stable ships.

### Files Affected
- `XamarinBackendService/XamarinBackendService.csproj` — only file changing in this task

