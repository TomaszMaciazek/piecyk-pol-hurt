namespace PiecykPolHurt.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PiecykPolHurt.API.Authorization;

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetPermissions()
        {
            return Ok(_user.Permissions);
        }
    }
}
