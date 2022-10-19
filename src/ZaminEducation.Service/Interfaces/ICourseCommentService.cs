using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.UserCourses;

namespace ZaminEducation.Service.Interfaces
{
    public interface ICourseCommentService
    {
        ValueTask<CourseComment> CreateAsync(long courseId, string message, long? parentId = null);
        ValueTask<bool> DeleteAsync(long id);
        ValueTask<IEnumerable<CourseComment>> GetAllAsync(PaginationParams @params, long courseId, string search = null);
        ValueTask<CourseComment> GetAsync(long id);
        ValueTask<CourseComment> UpdateAsync(long id, string message);
        ValueTask<IEnumerable<CourseComment>> GetReplies(long parentId);
    }
}