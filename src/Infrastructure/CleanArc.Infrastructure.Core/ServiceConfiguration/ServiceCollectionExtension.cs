using CleanArc.Domain.Contracts;
using CleanArc.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArc.Infrastructure.Identity.ServiceConfiguration;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterCoreServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderContract, OrderService>();
       

        return services;
    }

   
}