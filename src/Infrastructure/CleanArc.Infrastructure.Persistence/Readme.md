
# Db Migrations
Having Option to choose right database

## MS Sql Server

```
dotnet ef migrations add "Initial Migration" --context ApplicationDbContext --project ./src/Infrastructure/Migrations/CleanArc.Infrastructure.DbMigration.MSSQL/CleanArc.Infrastructure.DbMigration.MSSQL.csproj --startup-project ./src/Host/CleanArc.Web.Api/CleanArc.Web.Api.csproj  --verbose
```

## PostgreSql
```

dotnet ef migrations add "Initial Migration" --context ApplicationDbContext --project ./src/Infrastructure/Migrations/CleanArc.Infrastructure.DbMigration.Postgres/CleanArc.Infrastructure.DbMigration.Postgres.csproj --startup-project ./src/Host/CleanArc.Web.Api/CleanArc.Web.Api.csproj  --verbose
```