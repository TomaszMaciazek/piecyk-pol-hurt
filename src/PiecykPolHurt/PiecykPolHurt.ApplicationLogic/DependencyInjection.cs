namespace PiecykPolHurt.ApplicationLogic
{
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddLogic(this IServiceCollection services)
        {
            return services;
        }
    }
}