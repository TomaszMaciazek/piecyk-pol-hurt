using Microsoft.Extensions.DependencyInjection;

namespace PiecykPolHurt.ApplicationLogic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLogic(this IServiceCollection services)
        {
            return services;
        }
    }
}