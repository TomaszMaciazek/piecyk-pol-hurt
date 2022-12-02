using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PiecykPolHurt.DataLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}