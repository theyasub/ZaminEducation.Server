using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.Users;
using ZaminEducation.Service.DTOs.Users;
using ZaminEducation.Service.Interfaces;

namespace ZaminEducation.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserForCreationDto dto) =>
            Ok(await userService.CreateAsync(dto));

        [HttpDelete("{Id}"), Authorize(Roles = "Admin")]
        public async ValueTask<IActionResult> DeleteAsync(long id) =>
            Ok(await userService.DeleteAsync(user => user.Id == id));

        [HttpGet, Authorize]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params) =>
            Ok(await userService.GetAllAsync(@params));

        [HttpGet("Id")]
        public async ValueTask<IActionResult> GetAsync(long id) =>
            Ok(await userService.GetAsync(user => user.Id == id));

        [HttpPut, Authorize]
        public async ValueTask<ActionResult<User>> UpdateAsync(long id,
        [FromBody] UserForCreationDto dto) 
            => Ok(await userService.UpdateAsync(id, dto));
    }
}
