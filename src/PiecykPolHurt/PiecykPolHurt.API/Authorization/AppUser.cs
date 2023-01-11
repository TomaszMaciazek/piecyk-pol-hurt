namespace PiecykPolHurt.API.Authorization
{
    public class AppUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AppUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Email
        {
            get
            {
                var userEmail = _accessor.HttpContext?.User.Claims
                    .FirstOrDefault(c => c.Type == Claims.Email);

                return userEmail?.Value;
            }
        }

        public IEnumerable<string> Permissions
        {
            get
            {
                var permissions = _accessor.HttpContext?.User.Claims
                    .Where(c => c.Type == Claims.Permissions);

                return permissions.Select(x => x.Value);
            }
        }
    }
}
