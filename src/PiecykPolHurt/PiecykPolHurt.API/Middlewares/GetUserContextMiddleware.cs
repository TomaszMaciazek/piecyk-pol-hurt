using PiecykPolHurt.API.Authorization;
using System.Security.Claims;
using PiecykPolHurt.DataLayer;
using PiecykPolHurt.Model.Entities;
using Microsoft.Data.SqlClient;
using Dapper;

namespace PiecykPolHurt.API.Middlewares;

public class GetUserContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _connectionString;

    public GetUserContextMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var userEmail = context.User.Claims.FirstOrDefault(c => c.Type == Claims.Email)?.Value;

        if (!string.IsNullOrEmpty(userEmail))
        {
            var userId = await CheckIfUserShouldBeAddedToDb(userEmail);

            var userClaims = new List<Claim>()
                {
                    new(Claims.UserId, userId.ToString()),
                };

            context.User?.AddIdentity(new ClaimsIdentity(userClaims));
        }

        await _next(context);
    }
    private async Task<int> CheckIfUserShouldBeAddedToDb(string userEmail)
    {
        var addUserQuery = $"INSERT INTO Users (Email) VALUES ({userEmail})";
        var getUserQuery = $"SELECT Id, Email From Users WHERE Email = '{userEmail}'";

        await using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var user = (await connection.QueryAsync<User>(getUserQuery)).SingleOrDefault();

        if (user == null)
        {
            await connection.ExecuteAsync(addUserQuery);
        }

        user = (await connection.QueryAsync<User>(getUserQuery)).Single();
        return user.Id;
    }
}