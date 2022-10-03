
using System.Linq.Expressions;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.Courses;
using ZaminEducation.Domain.Entities.UserCourses;
using ZaminEducation.Service.DTOs.Courses;
using ZaminEducation.Service.ViewModels;

namespace ZaminEducation.Service.Interfaces.Courses
{
    public interface ICourseService
    {
        ValueTask<Course> CreateAsync(CourseForCreationDto courseForCreationDto);
        ValueTask<CourseViewModel> GetAsync(Expression<Func<Course, bool>> expression);
        ValueTask<IEnumerable<Course>> GetAllAsync(Expression<Func<Course, bool>> expression = null, PaginationParams @params = null);
        ValueTask<Course> UpdateAsync(Expression<Func<Course, bool>> expression, CourseForCreationDto courseForCreationDto);
        ValueTask<bool> DeleteAsync(Expression<Func<Course, bool>> expression);
        Task<IEnumerable<CourseVideo>> GetCourseVideosAsync(Expression<Func<Course, bool>> expression);
        Task<IEnumerable<CourseTarget>> GetCourseTargetsAsync(Expression<Func<Course, bool>> expression);
        Task<IEnumerable<CourseModule>> GetCourseModulesAsync(Expression<Func<Course, bool>> expression);
        Task<CourseRate> RateCoure(long id, byte value);
        Task<CourseRate> GetCourseRateOfUser(long id);
    }
}
