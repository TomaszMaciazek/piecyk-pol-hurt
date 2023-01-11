namespace PiecykPolHurt.API.Extensions
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using PiecykPolHurt.API.Authorization;
    using PiecykPolHurt.API.Middlewares;

    public static class AuthExtensions
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = configuration["Auth0:Domain"];
                options.Audience = configuration["Auth0:Audience"];
            });

            services.AddTransient<IUser, AppUser>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                    policy.RequireClaim(Claims.Permissions, "admin"));
                options.AddPolicy("Seller", policy =>
                    policy.RequireClaim(Claims.Permissions, "seller"));
            });
        }

        public static void UseAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseMiddleware<GetUserContextMiddleware>();
            app.UseAuthorization();
        }
    }
}
