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

        [HttpPost]
        public async ValueTask<ActionResult<ZCApplicantDirection>> CreateAsync(
            ZCApplicantDirectionForCreationDto dto)
            => Ok(await directionService.CreateAsync(dto));

        [HttpDelete]
        public async ValueTask<ActionResult<bool>> DeleteAsync(long id)
            => Ok(await directionService.DeleteAsync(c => c.Id == id));

        [HttpGet("id"), AllowAnonymous]
        public async ValueTask<ActionResult<ZCApplicantDirection>> GetAsync(long id)
            => Ok(await directionService.GetAsync(c => c.Id == id));

        [HttpPut]
        public async ValueTask<ActionResult<ZCApplicantDirection>> UpdateAsync(long id,
            ZCApplicantDirectionForCreationDto dto)
            => Ok(await directionService.UpdateAsync(c => c.Id == id, dto));

        [HttpGet, AllowAnonymous]
        public async ValueTask<ActionResult<IEnumerable<ZCApplicantDirection>>> GetAllAsync(
            PaginationParams @params)
            => Ok(await directionService.GetAllAsync(@params));
    }
}
