namespace PiecykPolHurt.API.Middlewares
{
    using PiecykPolHurt.API.Authorization;

    public class GetUserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public GetUserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userEmail = context.User.Claims.FirstOrDefault(c => c.Type == Claims.Email)?.Value;

            //TODO
            //Dodać do bazy danych jeżeli nie ma takiego użytkownika;

            await _next(context);
        }
    }
}
