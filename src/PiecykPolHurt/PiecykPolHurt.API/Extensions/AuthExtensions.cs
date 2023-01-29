using Microsoft.AspNetCore.Authentication.JwtBearer;
using PiecykPolHurt.API.Authorization;
using PiecykPolHurt.API.Middlewares;

namespace PiecykPolHurt.API.Extensions;

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
            options.Authority = configuration["Auth:Domain"];
            options.Audience = configuration["Auth:Audience"];
        });

        services.AddTransient<IUser, AppUser>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policy.Admin, policy =>
                policy.RequireClaim(Claims.Permissions, PermissionName.Admin));
            options.AddPolicy(Policy.Seller, policy =>
                policy.RequireClaim(Claims.Permissions, PermissionName.Seller));
        });
    }

    public static void UseAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseMiddleware<GetUserContextMiddleware>();
        app.UseAuthorization();
    }
}