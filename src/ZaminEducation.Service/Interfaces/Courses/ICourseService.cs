
using System.Linq.Expressions;
using ZaminEducation.Domain.Entities.Courses;
using ZaminEducation.Service.DTOs.CoursesDtos;

namespace ZaminEducation.Service.Interfaces.Courses
{
    public interface ICourseService
    {
        Task<Course> CreateAsync(CourseForCreationDto dto);
        Task<Course> GetAsync(Expression<Func<Course, bool>> expression);
        Task<IEnumerable<Course>> GetAllAsync(Expression<Func<Course, bool>> expression = null, string[] includes = null);
        Task<Course> UpdateAsync(Expression<Func<Course, bool>> expression, CourseCategoryForCreationDto dto);
        Task<bool> DeleteAsync(Expression<Func<Course, bool>> expression);
        Task<IEnumerable<CourseVideo>> GetCourseVideosAsync(Expression<Func<Course, bool>> expression);
        Task<IEnumerable<CourseTarget>> GetCourseTargetsAsync(Expression<Func<Course, bool>> expression);
        Task<IEnumerable<CourseModule>> GetCourseModulesAsync(Expression<Func<Course, bool>> expression);
    }
}
