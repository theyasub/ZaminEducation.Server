using System.Linq.Expressions;
using ZaminEducation.Domain.Entities.Courses;

namespace ZaminEducation.Service.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseVideo>> GetCourseVideosAsync(Expression<Func<Course, bool>> expression);
        Task<IEnumerable<CourseTarget>> GetCourseTargetsAsync(Expression<Func<Course, bool>> expression);
        Task<IEnumerable<CourseModule>> GetCourseModulesAsync(Expression<Func<Course, bool>> expression);
    }
}
