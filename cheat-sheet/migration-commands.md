* `$env:ASPNETCORE_ENVIRONMENT='Development'`

* `$env:ASPNETCORE_ENVIRONMENT='Production'`

* `dotnet ef migrations add "PLEASE_RENAME_ME" --project src\Infrastructure --startup-project src\WebAPI --output-dir Persistence\Migrations`

* `dotnet ef database update --project src\Infrastructure --startup-project src\WebAPI`
