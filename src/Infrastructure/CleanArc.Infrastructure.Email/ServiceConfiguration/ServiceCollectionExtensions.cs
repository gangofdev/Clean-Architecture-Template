using Microsoft.Extensions.DependencyInjection;

namespace CleanArc.Infrastructure.Email.ServiceConfiguration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterEmailServices(this IServiceCollection services)
        {
            return services;
        }
    }
}

