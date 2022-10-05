using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net.Mime;
using ZaminEducation.Api.Extensions;
using ZaminEducation.Data.DbContexts;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.Users;
using ZaminEducation.Service.DTOs.Users;
using ZaminEducation.Service.Interfaces;
using ZaminEducation.Service.Services;

namespace ZaminEducation.Api.Controllers;

public class UsersController : BaseController
{
    private readonly IUserService userService;
    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    /// <summary>
    /// create new user
    /// </summary>
    /// <param name="dto">user create</param>
    /// <returns>Created user infortaions</returns>
    /// <response code="200">If user is created successfully</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async ValueTask<ActionResult<User>> CreateAsync(UserForCreationDto dto) =>
        Ok(await userService.CreateAsync(dto));

    /// <summary>
    /// delete user
    /// </summary>
    /// <param name="id"></param>
    /// <returns>true if user deleted succesfully else false</returns>
    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    public async ValueTask<ActionResult<bool>> DeleteAsync([FromRoute]long id) =>
        Ok(await userService.DeleteAsync(user => user.Id == id));


    /// <summary>
    /// get all of users
    /// </summary>
    /// <param name="params">pagenation params</param>
    /// <returns> user collection </returns>
    [HttpGet, Authorize(Roles = "Admin")]
    public async ValueTask<ActionResult<IEnumerable<User>>> GetAllAsync(
        [FromQuery] PaginationParams @params) =>
            Ok(await userService.GetAllAsync(@params));
    
    /// <summary>
    /// update password
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("password"), Authorize("AllPolicy")]
    public async ValueTask<ActionResult<User>> ChangePasswordAsync(UserForChangePassword dto) =>
        Ok(await userService.ChangePasswordAsync(dto));

    /// <summary>
    /// get one user information
    /// </summary>
    /// <param name="id">user id</param>
    /// <returns>user</returns>
    /// <response code="400">if user data is not in the base</response>
    /// <response code="200">if user data have in database</response>
    [HttpGet("{id}"), Authorize("AllPolicy")]
    public async ValueTask<ActionResult<User>> GetAsync([FromRoute]long id) =>
        Ok(await userService.GetAsync(user => user.Id == id));

    /// <summary>
    /// update user 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut, Authorize("AllPolicy")]
    public async ValueTask<ActionResult<User>> UpdateAsync(
        long id, [FromBody] UserForUpdateDto dto) => 
            Ok(await userService.UpdateAsync(id, dto));

    /// <summary>
    /// get self user info
    /// </summary>
    /// <returns>user</returns>
    [HttpGet("info"), Authorize]
    public async ValueTask<ActionResult<User>> GetInfoAsync()
        => Ok(await userService.GetInfoAsync());

    /// <summary>
    /// create attachment
    /// </summary>
    /// <returns></returns>
    [HttpPost("attachments/{id}"), Authorize("UserPolicy")]
    public async Task<IActionResult> Attachment(long id, IFormFile formFile)
        => Ok(await userService.AddAttachmentAsync(id, formFile.ToAttachmentOrDefault()));
}

