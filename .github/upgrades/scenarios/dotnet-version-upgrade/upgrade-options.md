# Upgrade Options — XamarinBackendService

Assessment: 1 project (net4.6 → net10.0), 23 packages (18 incompatible), 3 controllers, 463 LOC, Azure Mobile Server SDK (deprecated), EF 6.1.3, System.Web

---

## Strategy

### Upgrade Strategy

| Value | Description |
|-------|-------------|
| **All-at-Once** (selected) | Single project — no dependency graph to manage. All changes applied in one pass. Fixed by framework migration rules. |

> Single-project solution. All-at-Once is fixed — no alternatives available.

---

## Project Structure

### Project Approach

| Value | Description |
|-------|-------------|
| **In-place rewrite** (selected) | Replace the ASP.NET Framework project (net4.6) with an SDK-style ASP.NET Core project (net10.0) in a single pass. Appropriate given: 3 controllers, 463 LOC, no production continuity requirements, and the entire Azure Mobile Server SDK must be removed regardless. |
| Side-by-side | Creates a new ASP.NET Core project alongside the old one. Better for large projects (>10 controllers) or when old app must stay live during migration. Not needed here. |

---

## Compatibility

### Unsupported Packages

| Value | Description |
|-------|-------------|
| **Resolve Inline** (selected) | Research and resolve all incompatible packages within the same task. 18 packages incompatible, but most have well-known replacements (ASP.NET Core built-ins for WebApi/OWIN, Microsoft.EntityFrameworkCore for EF, Microsoft.Extensions.Configuration for ConfigurationManager). Small project scope makes inline resolution tractable. |
| Defer Resolution | Generate stubs and defer resolution to follow-up tasks. Better for >3 packages without clear replacements. Not needed here — all packages have documented migration paths. |

### System.Web Adapters

| Value | Description |
|-------|-------------|
| **Direct Migration to ASP.NET Core APIs** (selected) | No `HttpContext.Current` usage found; only 3 small controllers; In-place rewrite approach confirmed. Replacing System.Web APIs directly with ASP.NET Core equivalents is cleaner and avoids a compatibility layer cleanup pass. |
| Use System.Web Adapters | Adds `Microsoft.AspNetCore.SystemWebAdapters` shims. Useful for large projects or side-by-side migration. Overkill for this small project. |

---

## Modernization

### Entity Framework

> **Note**: EF 6.1.3 is below 6.3 and does not support .NET Core. This is not configurable — EF6→EF Core migration is required and will be handled as part of the package resolution task.

| Value | Description |
|-------|-------------|
| **Migrate to EF Core** (selected — required) | EF 6.1.3 does not run on .NET Core (support added in EF6 6.3+). Migration to `Microsoft.EntityFrameworkCore` is mandatory. Data layer is small: 1 DbContext, 3 entity types — low risk. |

### Configuration Migration

| Value | Description |
|-------|-------------|
| **Auto-migrate to .NET Core Configuration** (selected) | `Web.config` has only 3 `appSettings` keys and 1 connection string. EntityFramework XML section not needed in EF Core. Standard migration to `appsettings.json` + `IConfiguration` is straightforward. |
| Manual Migration with Mapping Document | Adds a mapping review step. Warranted for complex configs (>20 keys, custom sections, encryption). Not needed here. |

### Nullable Reference Types

| Value | Description |
|-------|-------------|
| **Leave Disabled** (selected) | Migration involves high-risk breaking changes (entire SDK rewrite). Enable nullable separately as a follow-on effort after the project builds cleanly. |
| Enable Nullable Reference Types | Adds `<Nullable>enable</Nullable>`. Good for small clean codebases but adds noise during an already complex migration. |

---

**To proceed**: Reply `confirm` or `continue`
**To change**: Move `(selected)` to a different row or tell me what to adjust
