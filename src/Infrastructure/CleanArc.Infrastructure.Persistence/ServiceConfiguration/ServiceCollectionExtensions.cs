using CleanArc.Domain.Contracts.Persistence;
using CleanArc.Infrastructure.Persistence.Repositories.Common;
using CleanArc.SharedKernel.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CleanArc.Infrastructure.Persistence.ServiceConfiguration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var hostSettings = sp.GetRequiredService<IOptions<HostSettings>>()?.Value;

            switch (hostSettings.Database)
            {
                case HostDatabase.SqlServer:
                    options.UseSqlServer(configuration.GetConnectionString(nameof(HostDatabase.SqlServer)), builderOptions =>
                    {
                        builderOptions.MigrationsAssembly("CleanArc.Infrastructure.DbMigration.MSSQL");
                    });
                    break;
                case HostDatabase.Postgres:
                    options.UseNpgsql(configuration.GetConnectionString(nameof(HostDatabase.Postgres)), builderOptions =>
                    {
                        builderOptions.MigrationsAssembly("CleanArc.Infrastructure.DbMigration.Postgres");
                    });
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                    break;
                default:
                    options.UseInMemoryDatabase(nameof(HostDatabase.InMemory));
                    break;
            }
        });

        return services;
    }

    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        if (context is null)
            throw new Exception("Database Context Not Found");

        if (context.Database.ProviderName.Split(".").Last() != nameof(Microsoft.EntityFrameworkCore.InMemory))
        {
            await context.Database.MigrateAsync();
        }
    }
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var configuration = configurationBuilder
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .AddEnvironmentVariables()
                .Build();
            string database = configuration[$"{nameof(HostSettings)}:{nameof(HostSettings.Database)}"];
            Console.WriteLine($"DB Context Factory Selected DB: {database}");
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            switch (database)
            {
                case nameof(HostDatabase.SqlServer):
                    options.UseSqlServer(configuration.GetConnectionString(nameof(HostDatabase.SqlServer)), builderOptions =>
                    {
                        builderOptions.MigrationsAssembly("CleanArc.Infrastructure.DbMigration.MSSQL");
                    });
                    break;
                case nameof(HostDatabase.Postgres):
                    options.UseNpgsql(configuration.GetConnectionString(nameof(HostDatabase.Postgres)), builderOptions =>
                    {
                        builderOptions.MigrationsAssembly("CleanArc.Infrastructure.DbMigration.Postgres");
                    });
                    break;
                default:
                    options.UseInMemoryDatabase(nameof(HostDatabase.InMemory));
                    break;
            }


            return new ApplicationDbContext(options.Options);
        }
    }
}