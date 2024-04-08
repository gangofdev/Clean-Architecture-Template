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
                .Build();
            
           // var connectionString = configuration.GetConnectionString(_databaseName);

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("User ID=techdev;Password=techdev123;Host=localhost;Port=5432;Database=cleanarc_dev;Pooling=true;Connection Lifetime=0;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}