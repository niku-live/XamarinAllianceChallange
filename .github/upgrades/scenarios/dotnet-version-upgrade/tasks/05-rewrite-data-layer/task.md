# 05-rewrite-data-layer: Migrate EF context and entities to EF Core

Rewrite `XamarinBackendContext` and the `DataObjects` entity classes to remove the Azure Mobile Server SDK dependency (`EntityData`, `TableColumnAttribute`, `IAzureTableData`).

Changes required:
- `BaseDataObject`: Remove `EntityData` base class. Define own base with equivalent properties: `Id` (string), `CreatedAt` (DateTimeOffset?), `UpdatedAt` (DateTimeOffset?), `Version` (byte[]), `Deleted` (bool). These are the soft-delete/sync columns the Azure Mobile Client SDK expects in a standard REST API.
- `XamarinBackendContext`: Remove `Microsoft.Azure.Mobile.Server.Tables` conventions (`AttributeToColumnAnnotationConvention`, `TableColumnAttribute`). Replace with EF Core `DbContext` using `OnModelCreating` with standard EF Core Fluent API. Update connection string loading to use `IConfiguration` passed via DI.
- Entity classes (`Character`, `Movie`, `Weapon`): Remove `BaseDataObject` and inherit from the new base or use POCO. Verify navigation properties are correct for EF Core.
- Add EF Core migration or configure `EnsureCreated()` for development.

**Done when**: `XamarinBackendContext` compiles with EF Core only; all entities are POCO or use new base class; no references to `Microsoft.Azure.Mobile.Server` remain in the data layer; `dotnet build` succeeds for the data layer.
