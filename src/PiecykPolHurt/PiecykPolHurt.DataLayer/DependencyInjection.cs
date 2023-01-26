using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PiecykPolHurt.DataLayer.Common;
using PiecykPolHurt.DataLayer.Repositories;

namespace PiecykPolHurt.DataLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient, ServiceLifetime.Singleton);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}