

```dotnet ef migrations add "Initial Migration" --context ApplicationDbContext --project ./src/Infrastructure/CleanArc.Infrastructure.Persistence/CleanArc.Infrastructure.Persistence.csproj --startup-project ./src/Host/CleanArc.Web.Api/CleanArc.Web.Api.csproj -o PostgresMigrations --verbose```