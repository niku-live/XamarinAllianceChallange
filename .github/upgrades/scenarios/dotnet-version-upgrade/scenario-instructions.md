# .NET Version Upgrade — XamarinBackendService

## Preferences
- **Flow Mode**: Automatic

## Target Framework
- **Source**: net4.6 (ASP.NET Framework)
- **Target**: net10.0 (ASP.NET Core, LTS)

## Scope
- **In scope**: XamarinBackendService only
- **Out of scope**: Xamarin mobile projects (separate MAUI migration needed)

## Source Control
- **Source branch**: sdk-style-conversion
- **Working branch**: dotnet-version-upgrade
- **Pending changes at start**: None (committed)
- **Commit strategy**: After Each Task

## Upgrade Options

### Strategy
- **Upgrade Strategy**: All-at-Once (fixed — single project)

### Project Structure
- **Project Approach**: In-place rewrite (web project, 3 controllers, 463 LOC)

### Compatibility
- **Unsupported Packages**: Resolve Inline
- **System.Web Adapters**: Direct Migration to ASP.NET Core APIs

### Modernization
- **Entity Framework**: Migrate to EF Core (required — EF 6.1.3 cannot run on .NET Core)
- **Configuration Migration**: Auto-migrate to .NET Core Configuration (appsettings.json)
- **Nullable Reference Types**: Leave Disabled

## Strategy

**Selected**: All-at-Once
**Rationale**: Single project solution. Framework migration rules fix the strategy to All-at-Once — no dependency graph to manage.

### Execution Constraints
- Single atomic upgrade — all changes applied in one pass
- Validate full project build after upgrade before marking complete
- Operation order: project file → packages → restore → build + fix all errors
- No multi-targeting — in-place rewrite replaces net4.6 directly with net10.0
- EF Core migration is mandatory (EF 6.1.3 incompatible with .NET Core)

## Decisions
<!-- Decisions with context will be recorded here as they are made -->
