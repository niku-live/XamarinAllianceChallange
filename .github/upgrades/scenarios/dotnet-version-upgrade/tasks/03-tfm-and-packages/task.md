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
