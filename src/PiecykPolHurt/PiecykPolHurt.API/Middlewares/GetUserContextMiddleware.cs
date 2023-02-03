using PiecykPolHurt.API.Authorization;
using System.Security.Claims;
using PiecykPolHurt.DataLayer;
using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.API.Middlewares;

public class GetUserContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ApplicationDbContext _context;

    public GetUserContextMiddleware(RequestDelegate next, ApplicationDbContext context)
    {
        _next = next;
        _context = context;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var userEmail = context.User.Claims.FirstOrDefault(c => c.Type == Claims.Email)?.Value;

        if (!string.IsNullOrEmpty(userEmail))
        {
            var userId = CheckIfUserShouldBeAddedToDb(userEmail);

            var userClaims = new List<Claim>()
                {
                    new(Claims.UserId, userId.ToString()),
                };

            context.User?.AddIdentity(new ClaimsIdentity(userClaims));
        }

        await _next(context);
    }
    private int CheckIfUserShouldBeAddedToDb(string userEmail)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == userEmail);
        var newUser = new User
        {
            Email = userEmail,
        };

        if (user == null)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
        return user is null ? newUser.Id : user.Id;
    }
}