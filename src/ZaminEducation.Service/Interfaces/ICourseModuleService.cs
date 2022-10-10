
using System.Linq.Expressions;
using ZaminEducation.Domain.Entities.Courses;
using ZaminEducation.Service.DTOs.Courses;

namespace ZaminEducation.Service.Interfaces
{
    public interface ICourseModuleService
    {
        ValueTask<CourseModule> CreateAsync(CourseModuleForCreationDto dto);
        ValueTask<CourseModule> UpdateAsync(Expression<Func<CourseModule, bool>> expression, CourseModuleForCreationDto dto);
        ValueTask<bool> DeleteAsync(Expression<Func<CourseModule, bool>> expression);
    }
}
