using System.Linq.Expressions;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.Courses;

namespace ZaminEducation.Service.Interfaces
{
    public interface IYouTubeService
    {
        ValueTask<CourseVideo> CreateAsync(string link, long id);
        ValueTask<IEnumerable<CourseVideo>> CreateRangeAsync(string youtubePlaylist, long courseId);
        ValueTask<bool> DeleteAsync(long youtubeId);
        ValueTask<CourseVideo> UpdateAsync(long videoId, string link);
        ValueTask<CourseVideo> GetAsync(Expression<Func<CourseVideo, bool>> expression);
        ValueTask<IEnumerable<CourseVideo>> GetAllAsync(PaginationParams @params,
            Expression<Func<CourseVideo, bool>> expression = null);
        ValueTask<IEnumerable<string>> GetLinksAsync(string playlistLink);
        ValueTask SetModuleIdAsync(long[] Ids, long courseId, long moduleId);
    }
}
