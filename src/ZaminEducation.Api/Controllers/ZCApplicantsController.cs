using Microsoft.AspNetCore.Mvc;
using ZaminEducation.Api.Controllers;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.HomePage;
using ZaminEducation.Domain.Entities.Users;
using ZaminEducation.Service.DTOs.Users;
using ZaminEducation.Service.Interfaces;

namespace ZaminCreative.Api.Controllers
{
    public class ZCApplicantsController : BaseController
    {
        private readonly IZCApplicantService applicantUserService;

        public ZCApplicantsController(IZCApplicantService userService)
        {
            this.applicantUserService = userService;
        }

        /// <summary>
        /// Create a new applicant
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="file"></param>
        /// <returns>
        /// A created applicant
        /// </returns>
        [HttpPost]
        public async ValueTask<ActionResult<ZCApplicant>> CreateAsync(
            [FromForm] ZCApplicantForCreationDto dto, IFormFile file = null)
            => Ok(await applicantUserService.CreateAsync(dto,file?.OpenReadStream(),file?.FileName));

        /// <summary>
        /// Delete a applicant by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// A bool value depending on whether the condition is met
        /// </returns>
        [HttpDelete]
        public async ValueTask<ActionResult<bool>> DeleteAsync(long id)
            => Ok(await applicantUserService.DeleteAsync(u => u.Id == id));

        /// <summary>
        /// Update a applicant by entered id with entered dto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>
        /// Updated applicant
        /// </returns>
        [HttpPut]
        public async ValueTask<ActionResult<ZCApplicant>> UpdateAsync(
            long id,ZCApplicantForCreationDto dto)
            => Ok(await applicantUserService.UpdateAsync(u => u.Id == id, dto));

        /// <summary>
        /// Select a applicant by entered id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// a applicant depending on whether the condition is met
        /// </returns>
        [HttpGet("id")]
        public async ValueTask<ActionResult<ZCApplicant>> GetAsync(long id)
            => Ok(await applicantUserService.GetAsync(u => u.Id == id));

        /// <summary>
        /// Select all applicants
        /// </summary>
        /// <param name="params"></param>
        /// <returns>
        /// IEnumerable of applicants
        /// </returns>
        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<ZCApplicant>>> GetAllAsync([FromQuery]PaginationParams @params) 
            => Ok(await applicantUserService.GetAllAsync(@params));

        /// <summary>
        /// Create or Update a applicant info
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// Applicant info
        /// </returns>
        [HttpPost("page-info")]
        public async ValueTask<ActionResult<ZCApplicationInfo>> CreateHomePagesInfoAsync(ZCApplicationInfo entity)
            => Ok(await applicantUserService.CreateHomePageInfoAsync(entity));

        /// <summary>
        /// Delete a applicant info
        /// </summary>
        /// <returns>
        /// A bool value depending on whether the action was performed
        /// </returns>
        [HttpDelete("page-info")]
        public async ValueTask<ActionResult<bool>> DeleteHomePageInfoAsyncAsync()
            => Ok(await applicantUserService.DeleteHomePageInfoAsyncAsync());

        /// <summary>
        /// Select applicant info
        /// </summary>
        /// <returns></returns>
        [HttpGet("page-info")]
        public async ValueTask<ActionResult<ZCApplicationInfo>> GetHomePageInfoAsyncAsync()
            => Ok(await applicantUserService.GetHomePageInfoAsyncAsync());
    }
}
