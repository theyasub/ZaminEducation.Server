using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZaminEducation.Domain.Entities.MainPages;
using ZaminEducation.Service.DTOs.HomePage;
using ZaminEducation.Service.Interfaces;

namespace ZaminEducation.Api.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class HomePageController : BaseController
    {
        private readonly IHomePageService _homePageService;

        public HomePageController(IHomePageService homePageService)
        {
            _homePageService = homePageService;
        }

        /// <summary>
        /// take a landing page informations
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public async ValueTask<ActionResult<HomePage>> GetHomePageAsync()
        {
            return Ok(await _homePageService.GetHomePageAsync());
        }

        /// <summary>
        /// update main page header data 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPatch("header")]
        public async ValueTask<ActionResult<bool>> UpdateHomePageHeaderAsync(
            [FromForm] HomePageHeaderForCreationDto dto)
        {
            return Ok(await _homePageService.UpdateHeaderAsync(dto));
        }

        /// <summary>
        /// Update opportunity title data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPatch("opportunity")]
        public async ValueTask<ActionResult<bool>> UpdateOpportityAsync(
             [FromForm] OfferedOpportunitesForCreationDto dto)
        {
            return Ok(await _homePageService.UpdateOpportinutyAsync(dto));
        }

        /// <summary>
        /// Update photo gallery section images
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPatch("image/{id}")]
        public async ValueTask<ActionResult<bool>> UpdateGalleryImagesAsync(
            [FromRoute]long id, [FromForm] ImageForCreationDto dto)
        {
            return Ok(await _homePageService.UpdatePhotoGalleryAsync(id, dto));
        }

        /// <summary>
        /// update about us page data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPatch("about-us")]
        public async ValueTask<ActionResult<bool>> UpdateProjectAboutInfoAsync(
            [FromForm] InfoAboutProjectForCreationDto dto)
        {
            return Ok(await _homePageService.UpdateProjectAboutInfoAsync(dto));
        }

        /// <summary>
        /// update opportunity reasons  
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPatch("opportunity-reason/{id}")]
        public async ValueTask<ActionResult<bool>> UpdateOpportunityReasonAsync(
            [FromRoute]long id, ReasonForCreationDto dto)
        {
            return Ok(await _homePageService.UpdateReasonAsync(id, dto));
        }

        /// <summary>
        /// update social networks 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPatch("social-network")]
        public async ValueTask<ActionResult<bool>> UpdateSocialNetworksAsync(SocialNetworksForCreationDto dto)
        {
            return Ok(await _homePageService.UpdateSocialNetwordAsync(dto));
        }
    }
}
