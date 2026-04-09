# 02-sdk-style-conversion: Convert project to SDK-style format

Convert `XamarinBackendService.csproj` from the legacy MSBuild format (ToolsVersion="12.0", explicit `<Compile>` includes, `<Import>` of WebApplication targets) to SDK-style format while keeping the target framework at net4.6 for now.

This is a structural-only change — no TFM changes, no package changes yet. Remove:
- Explicit `<Compile Include="...">` entries (SDK-style uses globbing)
- Legacy MSBuild imports (`Microsoft.CSharp.targets`, `Microsoft.WebApplication.targets`)
- `packages.config` (replaced by `<PackageReference>` items added in the next task)
- `AssemblyInfo.cs` attributes that SDK auto-generates (`AssemblyVersion`, `AssemblyTitle`, etc.)
- Web.Debug.config / Web.Release.config XDT transforms (no equivalent in ASP.NET Core)

The project must restore and build (even if only partially — legacy references may not resolve) after conversion.

**Done when**: `XamarinBackendService.csproj` is SDK-style with `<Project Sdk="Microsoft.NET.Sdk.Web">`; `packages.config` removed; `AssemblyInfo.cs` cleaned of auto-generated attributes; project file is minimal and readable.
