using Microsoft.AspNetCore.Mvc;
using ZaminEducation.Data.IRepositories;
using ZaminEducation.Domain.Entities.Users;
using ZaminEducation.Service.DTOs.Users;
using ZaminEducation.Service.Interfaces;

namespace ZaminEducation.Api.Controllers
{
    [ApiController, Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IRepository<User> repository;
        public AuthController(IAuthService authService, IRepository<User> repository)
        {
            this.authService = authService;
            this.repository = repository;
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login(UserForLoginDTO dto)
            => Ok(new
            {
                toket = await authService.GenerateToken(dto.Login, dto.Password)
            });
    }
}
