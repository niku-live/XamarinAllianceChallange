# 05-rewrite-data-layer: Migrate EF context and entities to EF Core

Rewrite `XamarinBackendContext` and the `DataObjects` entity classes to remove the Azure Mobile Server SDK dependency (`EntityData`, `TableColumnAttribute`, `IAzureTableData`).

Changes required:
- `BaseDataObject`: Remove `EntityData` base class. Define own base with equivalent properties: `Id` (string), `CreatedAt` (DateTimeOffset?), `UpdatedAt` (DateTimeOffset?), `Version` (byte[]), `Deleted` (bool). These are the soft-delete/sync columns the Azure Mobile Client SDK expects in a standard REST API.
- `XamarinBackendContext`: Remove `Microsoft.Azure.Mobile.Server.Tables` conventions (`AttributeToColumnAnnotationConvention`, `TableColumnAttribute`). Replace with EF Core `DbContext` using `OnModelCreating` with standard EF Core Fluent API. Update connection string loading to use `IConfiguration` passed via DI.
- Entity classes (`Character`, `Movie`, `Weapon`): Remove `BaseDataObject` and inherit from the new base or use POCO. Verify navigation properties are correct for EF Core.
- Add EF Core migration or configure `EnsureCreated()` for development.

**Done when**: `XamarinBackendContext` compiles with EF Core only; all entities are POCO or use new base class; no references to `Microsoft.Azure.Mobile.Server` remain in the data layer; `dotnet build` succeeds for the data layer.

---

## Research Findings

### EF6 Usage Assessment (Step 1 of skill)
- No EDMX files — Code-First only ✅
- `XamarinBackendContext` has `DbSet<Movie>`, `DbSet<Weapon>`, `DbSet<Character>`
- `OnModelCreating` uses `AttributeToColumnAnnotationConvention<TableColumnAttribute>` — Mobile Server convention, must be removed
- No raw SQL, no stored procedures, no `Database.SetInitializer` calls in files (the one in `Startup.MobileApp.cs` is already deleted)
- No existing `Migrations/` folder — database is code-first without migrations (uses `EnsureCreated` or manual SQL)

### Package Status (already done in Task 03)
- ✅ `EntityFramework` 6.1.3 removed
- ✅ `Microsoft.EntityFrameworkCore.SqlServer` 9.0.4 added
- ✅ `Microsoft.EntityFrameworkCore.Tools` 9.0.4 added

### EntityData Properties to Replicate in BaseDataObject
The `EntityData` base class from Azure Mobile Server provides:
- `Id` (string, PK) — auto-set to GUID in constructor
- `Version` (byte[], `[Timestamp]` — row version concurrency token)
- `CreatedAt` (DateTimeOffset? — set on insert)
- `UpdatedAt` (DateTimeOffset? — updated on save)
- `Deleted` (bool — soft delete marker)
`BaseDataObject` also adds `RemoteId` (string) — keep this property.

### Navigation Properties (EF Core many-to-many)
- `Character` ↔ `Weapon` (many-to-many)
- `Character` ↔ `Movie` (many-to-many via `Appearances`)
EF Core 5+ supports implicit many-to-many without explicit join entity when there's no payload. Native `ICollection<T>` navigation properties on both sides are sufficient.

### Files to Rewrite
- `DataObjects/BaseDataObject.cs` — Replace `EntityData` with POCO base
- `DataObjects/Character.cs` — Remove Azure Mobile Server using
- `DataObjects/Movie.cs` — Remove Azure Mobile Server using
- `DataObjects/Weapon.cs` — Remove Azure Mobile Server using
- `Models/XamarinBackendContext.cs` — Full EF Core rewrite

