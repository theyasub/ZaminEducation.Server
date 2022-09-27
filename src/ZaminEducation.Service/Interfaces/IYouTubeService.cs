using System.Linq.Expressions;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.Courses;

namespace ZaminEducation.Service.Interfaces
{
    public interface IYouTubeService
    {
        Task<YouTubeVideo> CreateAsync(string link, long courseId);
        Task<IEnumerable<YouTubeVideo>> CreateRangeAsync(IEnumerable<string> links, long courseId);
        Task<bool> DeleteAsync(long id);
        Task<YouTubeVideo> UpdateAsync(long id, string link);
        Task<YouTubeVideo> GetAsync(Expression<Func<YouTubeVideo, bool>> expression);
        Task<IEnumerable<YouTubeVideo>> GetAllAsync(PaginationParams @params,
            Expression<Func<YouTubeVideo, bool>> expression);
        Task<long> AddToViewAsync(long id);
        Task<IEnumerable<string>> GetVideoLinksAsync(string playlistLink);
    }
}
