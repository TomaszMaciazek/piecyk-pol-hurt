using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PiecykPolHurt.DataLayer.Repositories;

namespace PiecykPolHurt.DataLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISendPointRepository, SendPointRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IDictionaryValueRepository, DictionaryValueRepository>();
            return services;
        }
    }
}