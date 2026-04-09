# 01-prerequisites: Verify SDK and toolchain

Confirm the .NET 10 SDK is correctly installed and accessible, verify the `dotnet` CLI can target `net10.0`, and establish a clean baseline by documenting the current state before any changes.

Check for a `global.json` file at the solution root that might pin an older SDK version and update it if needed. Record the current package restore state (packages.config + HintPath references) for reference during conversion.

**Done when**: `dotnet --version` reports a 10.x SDK; no `global.json` conflicts exist; baseline documented in task.md.

## Research Findings

- `dotnet --version`: **10.0.201** ✅
- `global.json`: **Not present** — no SDK pinning conflict ✅
- Build tool to use going forward: `dotnet build XamarinBackendService/XamarinBackendService.csproj` (project-scoped; solution cannot be loaded due to Xamarin PCL projects)
- Baseline: Legacy csproj format; packages.config with 23 packages; 18 incompatible with net10.0

