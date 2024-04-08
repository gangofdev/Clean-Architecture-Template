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
           var hostSettings= sp.GetRequiredService<IOptions<HostSettings>>()?.Value;
            
            switch (hostSettings.Database)
            {
                case HostDatabase.SqlServer:
                    options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
                    break;
                case HostDatabase.Postgres:
                    string connStr = configuration.GetConnectionString("Postgres");
                    options.UseNpgsql(configuration.GetConnectionString("Postgres"), builderOptions => {
                    
                    });
                    break;
                default:
                    options.UseInMemoryDatabase("InMemoryDb");
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
            Console.WriteLine($"DB Context Factory Selected DB: {configuration["HostSettings:Database"]}");
            string database = configuration["HostSettings:Database"];
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            switch (database)
            {
                case nameof(HostDatabase.SqlServer):
                    options.UseSqlServer(configuration.GetConnectionString("SqlServer"), builderOptions =>
                    {
                        builderOptions.MigrationsAssembly("CleanArc.Infrastructure.DbMigration.MSSQL");
                    });
                    break;
                case nameof(HostDatabase.Postgres):
                    string connStr = configuration.GetConnectionString("Postgres");
                    options.UseNpgsql(configuration.GetConnectionString("Postgres"), builderOptions =>
                    {
                        builderOptions.MigrationsAssembly("CleanArc.Infrastructure.DbMigration.Postgres");
                    });
                    break;
                default:
                    options.UseInMemoryDatabase("InMemoryDb");
                    break;
            }


            return new ApplicationDbContext(options.Options);
        }
    }
}