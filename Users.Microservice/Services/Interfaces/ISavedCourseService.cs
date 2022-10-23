using System.Linq.Expressions;
using Users.Microservice.Models.Configurations;
using Users.Microservice.Models.Entities;
using Users.Microservice.Services.DTOs;

namespace Users.Microservice.Services.Interfaces
{
    public interface ISavedCourseService
    {
        ValueTask<SavedCourse> GetAsync(Expression<Func<SavedCourse, bool>> expression = null);
        ValueTask<bool> ToggleAsync(SavedCourseForCreationDto dto, long userId);
        ValueTask<IEnumerable<SavedCourse>> GetAllAsync(PaginationParams @params, long userId, Expression<Func<SavedCourse, bool>> expression = null, string search = null);
    }
}
