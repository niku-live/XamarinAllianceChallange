# Task 05 — Rewrite Data Layer Progress Details

## What Changed

### `DataObjects/BaseDataObject.cs`
**Before**: Extended `EntityData` from `Microsoft.Azure.Mobile.Server` SDK, which defined `Id`, `Version`, `CreatedAt`, `UpdatedAt`, `Deleted`.

**After**: Self-contained POCO with all properties defined explicitly. Removed `EntityData` base class and `Microsoft.Azure.Mobile.Server` using. Added `[Key]` on `Id` and `[Timestamp]` on `Version` (row version concurrency token) using EF Core data annotations.

```csharp
[Key] public string Id { get; set; }
[Timestamp] public byte[] Version { get; set; }
public DateTimeOffset? CreatedAt { get; set; }
public DateTimeOffset? UpdatedAt { get; set; }
public bool Deleted { get; set; }
public string RemoteId { get; set; }
```

### `DataObjects/Character.cs`, `Movie.cs`, `Weapon.cs`
Removed `using Microsoft.Azure.Mobile.Server;` — the only Azure Mobile Server reference was for `EntityData` via `BaseDataObject`. All entity properties and navigation properties (`ICollection<T>`) are unchanged.

### `Models/XamarinBackendContext.cs`
**Before**: EF6 `DbContext` with connection string name in constructor and `AttributeToColumnAnnotationConvention<TableColumnAttribute>` convention.

**After**: EF Core `DbContext` accepting `DbContextOptions<XamarinBackendContext>` via DI (registered in `Program.cs`). `OnModelCreating` override retained (just calls base for now — EF Core convention-based configuration handles the three entity sets automatically).

```csharp
public XamarinBackendContext(DbContextOptions<XamarinBackendContext> options) : base(options) { }
```

`OnModelCreating` parameter changed from `DbModelBuilder` → `ModelBuilder`. Removed `AttributeToColumnAnnotationConvention`.

## Build Status

Data layer files (DataObjects/, Models/) → **0 compilation errors** ✅

Remaining build errors are only in:
- `Controllers/` (3 controllers — `TableController<T>`, `System.Web.Http.*`, `SingleResult<>`)
- `Helpers/QueryableExpandAttribute.cs` (`System.Web.Http.Filters`, `ActionFilterAttribute`)

All these are resolved in Task 06.
