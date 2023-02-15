using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PiecykPolHurt.Email
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEmails(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailConfiguration>(x => new EmailConfiguration(configuration["Email:Host"], configuration["Email:User"], configuration["Email:Password"]));
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}