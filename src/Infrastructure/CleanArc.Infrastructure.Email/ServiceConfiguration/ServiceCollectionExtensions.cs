using CleanArc.Domain.Contracts.Email;
using CleanArc.Infrastructure.Email.MailKit;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArc.Infrastructure.Email.ServiceConfiguration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterEmailServices(this IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, MailKitMailSender>();

            return services;
        }
    }
}

