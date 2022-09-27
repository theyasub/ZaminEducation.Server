using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.UserCourses;
using ZaminEducation.Service.DTOs.UserCourses;

namespace ZaminEducation.Service.Interfaces
{
    public interface ISavedCoursesService
    {
        ValueTask<SavedCourse> GetAsync(Expression<Func<SavedCourse, bool>> expression = null);
        ValueTask<bool> AddRemoveAsync(SavedCourseForCreationDto dto);
        // ValueTask<bool> RemoveAsync(Expression<Func<SavedCourse, bool>> expression);
        ValueTask<IEnumerable<SavedCourse>> GetAllAsync(PaginationParams @params, Expression<Func<SavedCourse, bool>> expression = null);
    }
}