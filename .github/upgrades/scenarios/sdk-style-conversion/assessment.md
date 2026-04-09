# Assessment: SDK-style Conversion

## Baseline
- Solution builds: Unknown (no build attempted — Xamarin tooling not installed in this environment)
- Warning count: N/A

## Projects Analyzed

| Project | Path | packages.config | Custom Imports | Special Type | Risk |
|---------|------|----------------|----------------|-------------|------|
| XamarinAllianceApp | XamarinAllianceApp/XamarinAllianceApp.csproj | Yes | Xamarin.Forms.targets + Microsoft.Portable.CSharp.targets | Portable Class Library (Profile259) | **Very High** |
| XamarinAllianceApp.Droid | Droid/XamarinAllianceApp.Droid.csproj | Yes | Xamarin.Android targets | Xamarin.Android application | **Not Feasible** |
| XamarinAllianceApp.iOS | iOS/XamarinAllianceApp.iOS.csproj | Yes | Xamarin.iOS targets | Xamarin.iOS application | **Not Feasible** |
| XamarinAllianceApp.UWP | UWP/XamarinAllianceApp.UWP.csproj | No (project.json) | None | UWP / UAP application | **Not Feasible** |
| XamarinBackendService | XamarinBackendService/XamarinBackendService.csproj | Yes (23 packages) | None critical | ASP.NET Framework Web API (net46) | **High** |

## Already SDK-style
- None

## Key Findings

### XamarinAllianceApp (Portable Class Library)
- Targets PCL Profile259 (`net45+wp80+win81+wpa81+MonoAndroid10+MonoTouch10+Xamarin.iOS10`)
- Has explicit `<Compile>` includes for all source files + `AssemblyInfo.cs` (duplication risk)
- Imports `Xamarin.Forms.targets` — these are Xamarin-specific build targets only available in old project format
- XAML files present with Xamarin.Forms bindings
- Converting to SDK-style would require changing target to `netstandard2.0`, which is the `dotnet-version-upgrade` scenario scope, not sdk-style-conversion
- **Assessment**: Technically possible only as part of a MAUI migration, not standalone

### XamarinAllianceApp.Droid (Xamarin.Android)
- ProjectTypeGuid `{EFBA0AD7-5A72-4C68-AF49-83D382785DCF}` identifies as Xamarin.Android
- Requires Mono.Android runtime and Xamarin.Android MSBuild SDK
- SDK-style format for Android is only supported via .NET MAUI (`net8-android` / `net9-android` TFMs)
- **Assessment**: Cannot be converted to SDK-style without a full migration to .NET MAUI

### XamarinAllianceApp.iOS (Xamarin.iOS)
- ProjectTypeGuid `{FEACFBD2-3405-455C-9665-78FE426C6842}` identifies as Xamarin.iOS
- Same situation as Android: SDK-style only available via .NET MAUI (`net8-ios` / `net9-ios`)
- **Assessment**: Cannot be converted to SDK-style without a full migration to .NET MAUI

### XamarinAllianceApp.UWP (Universal Windows Platform)
- Uses `TargetPlatformIdentifier=UAP` — the old Universal App Platform SDK
- Uses `project.json` for NuGet (very early NuGet 3 format, predates PackageReference)
- UWP SDK-style support is limited and unreliable with `dotnet build`
- Modern equivalent is Windows App SDK / WinUI 3
- **Assessment**: Not feasible for SDK-style conversion without platform migration

### XamarinBackendService (ASP.NET Framework Web API)
- ProjectTypeGuid `{349c5851-65df-11da-9384-00065b846f21}` → ASP.NET web project type
- Targets net46, uses System.Web (not available in SDK-style + .NET Core)
- 23 packages in packages.config including Azure Mobile Server SDK (deprecated), EF6, WebApi OData
- `dotnet build` does NOT support SDK-style + ASP.NET Framework (System.Web dependency)
- **Assessment**: Conversion is technically possible but `dotnet build` / VS tooling will not work correctly; this is documented as unsupported by the scenario. Proper modernization requires migrating to ASP.NET Core.

## Recommended Modernization Path

SDK-style conversion in isolation is **not the right first step** for this solution. The Xamarin platform itself is end-of-life (Microsoft ended Xamarin support in May 2024), and all projects are tied to platform-specific tooling that is incompatible with SDK-style format without framework migration.

**Recommended sequence:**
1. **Backend** (`XamarinBackendService`): Use `dotnet-version-upgrade` scenario to migrate to ASP.NET Core (net8/net9), which naturally produces SDK-style output
2. **Mobile apps**: Use the `azure-migrate` scenario or manual migration guide to migrate from Xamarin.Forms to .NET MAUI (which uses SDK-style natively with `net*-android`, `net*-ios`, `net*-maccatalyst` TFMs)

SDK-style format comes naturally as part of these migrations — it need not and should not be done as a separate isolated step given this codebase.
