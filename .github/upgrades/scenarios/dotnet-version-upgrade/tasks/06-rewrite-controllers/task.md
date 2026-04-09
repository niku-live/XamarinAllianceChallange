# 06-rewrite-controllers: Replace TableController with ControllerBase

Rewrite all three controllers (`CharacterController`, `MovieController`, `WeaponController`) to replace `TableController<T>` (Azure Mobile Server) with standard ASP.NET Core `ControllerBase`.

Each controller must:
- Inherit from `ControllerBase` with `[ApiController]` and `[Route("tables/[controller]")]` attributes
- Accept `XamarinBackendContext` via constructor injection (DI)
- Implement GET (all), GET (by id) using EF Core LINQ queries
- Apply `[EnableQuery]` from `Microsoft.AspNetCore.OData` for OData query support (replaces `[QueryableExpand]` custom attribute)
- Remove the `QueryableExpandAttribute` helper (replaced by OData's built-in `[EnableQuery]`)

The in-memory test data (`_test` list) currently in `CharacterController` can remain as the default data source or be replaced with EF Core queries — follow what was already partially implemented (the commented-out `Query()` / `Lookup()` calls suggest EF was the intent).

**Done when**: All 3 controllers compile; no references to `Microsoft.Azure.Mobile.Server` remain; each controller has working GET endpoints; `QueryableExpandAttribute` removed; `dotnet build` reports 0 errors and 0 warnings for the project.

---

## Research Findings

### Controller Analysis
All 3 controllers extend `TableController<T>` from Azure Mobile Server SDK. Key replacements:
- `Initialize(HttpControllerContext)` + `DomainManager = new EntityDomainManager<T>(context, Request)` → constructor injection of `XamarinBackendContext`
- `Query()` / `Lookup(id)` built-in methods → direct EF Core LINQ (`_context.Characters`, `.Where(x => x.Id == id)`)
- `SingleResult<T>` return type → `IQueryable<T>` filtered by ID (OData handles the envelope)
- `[QueryableExpand("Weapons,Appearances")]` → `[EnableQuery]` (standard OData; keep `.Include()` for always-expanding nav properties per original behavior)
- `new XamarinBackendContext()` (direct instantiation) → constructor injection via DI

### CharacterController Special Case
Has a `_test` in-memory list. The commented-out EF calls (`Query()`, `Lookup()`) show EF was the intent. Replacing _test with EF Core queries.

### Navigation Property Expansion
Original used `[QueryableExpand]` to force include `Weapons,Appearances` and `Characters` always. In the new implementation:
- Use EF Core `.Include()` always to maintain backward compatibility with Xamarin client (client doesn't send `$expand`)
- `[EnableQuery]` still allows OData filtering/ordering

### Files to Change
- **Rewrite**: `Controllers/CharacterController.cs`, `Controllers/MovieController.cs`, `Controllers/WeaponController.cs`
- **Delete**: `Helpers/QueryableExpandAttribute.cs` (replaced by `[EnableQuery]`)

### Key Namespace
`[EnableQuery]` is from `Microsoft.AspNetCore.OData.Query` namespace.

