using Microsoft.Extensions.DependencyInjection;
using PiecykPolHurt.ApplicationLogic.Services;

namespace PiecykPolHurt.ApplicationLogic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLogic(this IServiceCollection services)
        {
            services.AddScoped<ISendPointService, SendPointService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IProductSendPointService, ProductSendPointService>();
            return services;
        }
    }
}