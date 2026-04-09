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
