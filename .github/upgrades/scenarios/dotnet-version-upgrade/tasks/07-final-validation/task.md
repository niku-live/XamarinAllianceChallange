# 07-final-validation: Full build and smoke test

Validate the complete migration: build the project, run any existing tests, and document the final state.

- Run `dotnet build XamarinBackendService/XamarinBackendService.csproj` — must succeed with 0 errors and 0 warnings
- Run `dotnet run` briefly to verify the application starts without exceptions
- Verify OData endpoint metadata is reachable (`/odata/$metadata` or equivalent)
- Document any deferred recommendations (e.g., enabling nullable reference types, adding integration tests, EF Core migration scripts for production)
- Commit all changes to the `dotnet-version-upgrade` branch

**Done when**: Project builds with 0 errors and 0 warnings; application starts successfully; no references to `Microsoft.Azure.Mobile.Server`, `System.Web`, or `Owin` remain anywhere in the project; commit made on working branch.
