using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Microservice.Extentions.Attributes;
using Users.Microservice.Helpers;
using Users.Microservice.Models.Configurations;
using Users.Microservice.Models.Entities;
using Users.Microservice.Models.Enums;
using Users.Microservice.Services.DTOs;
using Users.Microservice.Services.Extentions;
using Users.Microservice.Services.Interfaces;

namespace Users.Microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ISavedCourseService savedCoursesService;
        public UsersController(IUserService userService, ISavedCourseService savedCoursesService)
        {
            this.userService = userService;
            this.savedCoursesService = savedCoursesService;
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="dto">user create</param>
        /// <returns>Created user infortaions</returns>
        /// <response code="200">If user is created successfully</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<User>> CreateAsync(UserForCreationDto dto) =>
            Ok(await userService.CreateAsync(dto));

        /// <summary>
        /// Update role 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPut("change/role"), Authorize(Roles = CustomRoles.SuperAdminRole)]
        public async ValueTask<ActionResult<User>> ChangeRoleAsync(long userId, UserRole role)
            => Ok(await userService.ChangeRoleAsync(userId, role));

        /// <summary>
        /// Toggle saved course
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("saved-course")]
        public async ValueTask<ActionResult<SavedCourse>> ToggleAsync(SavedCourseForCreationDto dto) =>
            Ok(await savedCoursesService.ToggleAsync(dto, long.Parse(User.FindFirst("Id").Value)));

        /// <summary>
        /// Delete user by id (for only admins)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if user deleted succesfully else false</returns>
        [HttpDelete("{id}"), Authorize(Roles = CustomRoles.AdminRole)]
        public async ValueTask<ActionResult<bool>> DeleteAsync([FromRoute] long id) =>
            Ok(await userService.DeleteAsync(user => user.Id == id));


        /// <summary>
        /// Get all of users
        /// </summary>
        /// <param name="params">pagenation params</param>
        /// <returns> user collection </returns>
        [HttpGet, Authorize(Roles = CustomRoles.AdminRole)]
        public async ValueTask<ActionResult<IEnumerable<User>>> GetAllAsync(
            [FromQuery] PaginationParams @params) =>
                Ok(await userService.GetAllAsync(@params));

        /// <summary>
        /// Get all saved courses of users
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet("saved-course"), Authorize]
        public async ValueTask<ActionResult<IEnumerable<SavedCourse>>> GetAllSavedCoursesAsync(
            [FromQuery] PaginationParams @params, string search) =>
                Ok(await savedCoursesService.GetAllAsync(@params, long.Parse(User.FindFirst("Id").Value), search: search));

        /// <summary>
        /// Update password
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("password"), Authorize]
        public async ValueTask<ActionResult<User>> ChangePasswordAsync(UserForChangePassword dto) =>
            Ok(await userService.ChangePasswordAsync(dto));

        /// <summary>
        /// Get one user information
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user</returns>
        /// <response code="400">if user data is not in the base</response>
        /// <response code="200">if user data have in database</response>
        [HttpGet("{id}"), Authorize(Roles = CustomRoles.AllRoles)]
        public async ValueTask<ActionResult<User>> GetAsync([FromRoute] long id) =>
            Ok(await userService.GetAsync(user => user.Id == id));

        /// <summary>
        /// Update user 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}"), Authorize(Roles = CustomRoles.AllRoles)]
        public async ValueTask<ActionResult<User>> UpdateAsync(
            long id, [FromBody] UserForUpdateDto dto) =>
                Ok(await userService.UpdateAsync(id, dto));

        /// <summary>
        /// Get self user info
        /// </summary>
        /// <returns>user</returns>
        [HttpGet("info"), Authorize]
        public async ValueTask<ActionResult<User>> GetInfoAsync() =>
            Ok(await userService.GetInfoAsync(long.Parse(User.FindFirst("Id").Value)));

        /// <summary>
        /// Create attachment
        /// </summary>
        /// <returns></returns>
        [HttpPost("attachments/{id}"), Authorize(Roles = CustomRoles.UserRole)]
        public async Task<IActionResult> Attachment(long id, 
            [FormFileAttributes, IsNoMoreThenMaxSize(3145728)] IFormFile formFile) =>
            Ok(await userService.AddAttachmentAsync(id, formFile.ToAttachmentOrDefault()));
    }
}
