using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZaminEducation.Api.Helpers;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.user;
using ZaminEducation.Service.DTOs.Users;
using ZaminEducation.Service.Interfaces;

namespace ZaminEducation.Api.Controllers
{
    [Authorize(Roles = CustomRoles.AdminRole)]
    public class ZCApplicantDirectionsController : BaseController
    {
        private readonly IZCApplicantDirectionService directionService;

        public ZCApplicantDirectionsController(IZCApplicantDirectionService directionService)
        {
            this.directionService = directionService;
        }

        /// <summary>
        /// Create a new direction for ZaminCreative
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>
        /// created direction
        /// </returns>
        [HttpPost]
        public async ValueTask<ActionResult<ZCApplicantDirection>> CreateAsync(
            ZCApplicantDirectionForCreationDto dto)
            => Ok(await directionService.CreateAsync(dto));

        /// <summary>
        /// Delete a direction
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// A bool value depending on whether the condition is met
        /// </returns>
        [HttpDelete("{id}")]
        public async ValueTask<ActionResult<bool>> DeleteAsync(long id)
            => Ok(await directionService.DeleteAsync(c => c.Id == id));

        /// <summary>
        /// Select a direction by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Selects a direction with the entered id
        /// </returns>
        [HttpGet("{id}"), AllowAnonymous]
        public async ValueTask<ActionResult<ZCApplicantDirection>> GetAsync(long id)
            => Ok(await directionService.GetAsync(c => c.Id == id));

        /// <summary>
        /// Update a direction by id with entered dto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>
        /// Selects an updated direction 
        /// </returns>
        [HttpPut("{id}")]
        public async ValueTask<ActionResult<ZCApplicantDirection>> UpdateAsync(long id,
            ZCApplicantDirectionForCreationDto dto)
            => Ok(await directionService.UpdateAsync(c => c.Id == id, dto));

        /// <summary>
        /// Selects all directions
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public async ValueTask<ActionResult<IEnumerable<ZCApplicantDirection>>> GetAllAsync(
            PaginationParams @params)
            => Ok(await directionService.GetAllAsync(@params));
    }
}
