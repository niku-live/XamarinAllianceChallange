# Task 03 — TFM and Package Replacement Progress Details

## What Changed

### Target Framework
Changed `<TargetFramework>` from `net46` to `net10.0`.

### Package Summary: 19 → 7 packages

**Removed (14 packages):**
- `Microsoft.Azure.Mobile.Server` 2.0.0
- `Microsoft.Azure.Mobile.Server.Authentication` 2.0.0
- `Microsoft.Azure.Mobile.Server.Entity` 2.0.0
- `Microsoft.Azure.Mobile.Server.Home` 2.0.0
- `Microsoft.Azure.Mobile.Server.Quickstart` 2.0.0
- `Microsoft.Azure.Mobile.Server.Tables` 2.0.0
- `Microsoft.AspNet.WebApi.Core` 5.2.3
- `Microsoft.AspNet.WebApi.Client` 5.2.3
- `Microsoft.AspNet.WebApi.Owin` 5.2.3
- `Microsoft.AspNet.WebApi.Tracing` 5.2.3
- `Microsoft.AspNet.WebApi.OData` 5.7.0 + `Microsoft.Data.OData` 5.8.4 + `Microsoft.Data.Edm` 5.8.1 + `System.Spatial` 5.8.1
- `EntityFramework` 6.1.3
- `Microsoft.WindowsAzure.ConfigurationManager` 3.2.3
- `System.IdentityModel.Tokens.Jwt` 5.7.0 (replaced with 8.14.0)

**Kept with update:**
- `Newtonsoft.Json` 13.0.1 → 13.0.3
- `AutoMapper` 5.2.0 → 16.1.1 (latest, vulnerability-free)

**Added:**
- `Microsoft.AspNetCore.OData` 9.3.0
- `Microsoft.EntityFrameworkCore.SqlServer` 9.0.4
- `Microsoft.EntityFrameworkCore.Tools` 9.0.4 (PrivateAssets=all)
- `Microsoft.IdentityModel.Tokens` 8.14.0
- `System.IdentityModel.Tokens.Jwt` 8.14.0

### Version Resolution Notes
- AutoMapper 16.1.1 requires `Microsoft.IdentityModel.Tokens >= 8.14.0` → updated to 8.14.0 to avoid NU1605 downgrade error
- `Microsoft.AspNetCore.OData` 9.2.2 was not available on NuGet → updated to 9.3.0 (which is the resolution NuGet used anyway)

## Build Status

`dotnet restore` → **Clean** (0 warnings, 0 errors)

Build at this stage produces compilation errors for the source files that still reference removed Azure Mobile Server SDK types (`TableController<T>`, `EntityDomainManager<T>`, `IAppBuilder`, EF6 `DbContext`, etc.) — all resolved in Tasks 04–06.

## Final csproj State

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <RootNamespace>XamarinBackendService</RootNamespace>
    <AssemblyName>XamarinBackendService</AssemblyName>
    <TargetFramework>net10.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <!-- App_Data and SQL script content items -->
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include="AutoMapper" Version="16.1.1" />
    <PackageReference Include="Microsoft.AspNetCore.OData" Version="9.3.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.4" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.4">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.IdentityModel.Tokens" Version="8.14.0" />
    <PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
    <PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="8.14.0" />
  </ItemGroup>
</Project>
```
