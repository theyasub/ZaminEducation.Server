using Microsoft.AspNetCore.Mvc;
using Users.Microservice.Services.Interfaces;

namespace Users.Microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]/login")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<string>> Login(string userName, string password)
            => Ok(await authService.GenereteToken(userName, password));
    }
}
