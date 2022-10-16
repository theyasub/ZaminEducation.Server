
using Microsoft.AspNetCore.Mvc;
using ZaminEducation.Service.DTOs.Courses;
using ZaminEducation.Service.Interfaces;
using ZaminEducation.Service.Interfaces.Courses;

namespace ZaminEducation.Api.Controllers
{
    public class CourseModulesController : BaseController
    {
        ICourseModuleService courseModuleService;
        ICourseService courseService;

        public CourseModulesController(ICourseModuleService courseModuleService, ICourseService courseService)
        {
            this.courseModuleService = courseModuleService;
            this.courseService = courseService;
        }

        /// <summary>
        /// Create module
        /// </summary>
        /// <param name="courseModuleForCreationDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(CourseModuleForCreationDto courseModuleForCreationDto)
            => Ok(await this.courseModuleService.CreateAsync(courseModuleForCreationDto));

        /// <summary>
        /// Create multiple modules
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="moduleNames"></param>
        /// <returns></returns>
        [HttpPost("{courseId}/collection")]
        public async ValueTask<IActionResult> CreateRangeAsync(long courseId, IEnumerable<string> moduleNames)
            => Ok(await this.courseModuleService.CreateRangeAsync(courseId, moduleNames));

        /// <summary>
        /// Get all modules
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("{courseId}")]
        public async ValueTask<IActionResult> RetrieveAllAsync(long courseId)
            => Ok(await this.courseModuleService.GetAllAsync(courseId, c => c.Id.Equals(courseId)));

        /// <summary>
        /// Remove module
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [HttpDelete("{courseId}/{moduleId}")]
        public async ValueTask<IActionResult> RemoveAsync(long courseId, long moduleId)
            => Ok(await this.courseModuleService.DeleteAsync(cm => cm.CourseId.Equals(courseId) && cm.Id.Equals(moduleId)));
        
        /// <summary>
        /// Update module
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{courseId}/{moduleId}")]
        public async ValueTask<IActionResult> UpdateAsync(long moduleId, 
                                                 CourseModuleForCreationDto dto)
            => Ok(await this.courseModuleService.UpdateAsync(cm => cm.CourseId.Equals(dto.CourseId) &&
                                                                   cm.Id.Equals(moduleId), dto));
    }
}
