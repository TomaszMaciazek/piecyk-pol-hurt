using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PiecykPolHurt.EmailService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailService, EmailService>(_ => new EmailService(configuration["Azure:SendEmailUrl"]));
            return services;
        }
    }
}