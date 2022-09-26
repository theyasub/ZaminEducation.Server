using System.Linq.Expressions;
using YoutubeExplode;
using YoutubeExplode.Common;
using ZaminEducation.Data.IRepositories;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.Courses;
using ZaminEducation.Service.Exceptions;
using ZaminEducation.Service.Extensions;
using ZaminEducation.Service.Interfaces;

#pragma warning disable
namespace ZaminEducation.Service.Services
{
    public class YouTubeService: IYouTubeService
    {
        private readonly IRepository<YouTubeVideo> repository;

        public YouTubeService(IRepository<YouTubeVideo> repository)
        {
            this.repository = repository;
        }

        public async Task<YouTubeVideo> CreateAsync(string link, long courseId)
        {
            var course = await repository.GetAsync(course => course.Id == courseId);

            if (course is null)
                throw new ZaminEducationException(404, "Course not found!");

            var video = await new YoutubeClient().Videos.GetAsync(YouTubeVideoIdExtractor(link));

            var youtubeVideo = new YouTubeVideo
            {
                CourseId = courseId,
                Thumbnail = video.Thumbnails.OrderByDescending(p => p.Resolution.Height).FirstOrDefault()?.Url,
                Title = video.Title,
                Url = video.Url,
                ViewCount = 0,
                Length = video.Duration!.Value.Minutes,
                Description = video.Description
            };

            youtubeVideo = await repository.AddAsync(youtubeVideo);

            await repository.SaveChangesAsync();

            return youtubeVideo;
        }

        public async Task<IEnumerable<YouTubeVideo>> CreateRangeAsync(IEnumerable<string> links, long courseId)
        {
            var result = new List<YouTubeVideo>();
            var yt = new YoutubeClient();

            foreach (var link in links)
            {
                var video = await yt.Videos.GetAsync(link);

                var youtubeVideo = new YouTubeVideo
                {
                    CourseId = courseId,
                    Thumbnail = video.Thumbnails.OrderByDescending(p => p.Resolution.Height).FirstOrDefault()?.Url,
                    Title = video.Title,
                    Url = video.Url,
                    ViewCount = 0,
                    Length = video.Duration!.Value.Minutes,
                    Description = video.Description
                };

                result.Add(await repository.AddAsync(youtubeVideo));
            }

            await repository.SaveChangesAsync();

            return result;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existVideo = await repository.GetAsync(p => p.Id == id);

            if (existVideo is null)
                throw new ZaminEducationException(404, "Video not found!");

            repository.Delete(existVideo);

            await repository.SaveChangesAsync();

            return true;
        }

        public async Task<YouTubeVideo> UpdateAsync(long id, string link)
        {
            var existVideo = await repository.GetAsync(p => p.Id == id);

            if (existVideo is null)
                throw new ZaminEducationException(400, "Video not found!");

            var video = await new YoutubeClient().Videos.GetAsync(YouTubeVideoIdExtractor(link));

            var youtubeVideo = new YouTubeVideo
            {
                Thumbnail = video.Thumbnails.OrderByDescending(p => p.Resolution.Height).FirstOrDefault()?.Url,
                Title = video.Title,
                Url = video.Url,
                ViewCount = 0,
                Length = video.Duration!.Value.Minutes,
                Description = video.Description,
                CourseId = existVideo.CourseId,
                CreatedAt = existVideo.CreatedAt,
                UpdatedAt = existVideo.UpdatedAt,
                Id = existVideo.Id
            };

            youtubeVideo = repository.Update(youtubeVideo);

            await repository.SaveChangesAsync();

            return youtubeVideo;
        }

        public async Task<YouTubeVideo> GetAsync(Expression<Func<YouTubeVideo, bool>> expression)
            => await repository.GetAsync(expression);

        public async Task<IEnumerable<YouTubeVideo>> GetAllAsync(PaginationParmas @params,
            Expression<Func<YouTubeVideo, bool>> expression)
            => repository.GetAll(expression).ToPageList(@params);

        public async Task<long> AddToViewAsync(long id)
        {
            var existVideo = await repository.GetAsync(p => p.Id == id);

            if (existVideo is null)
                throw new ZaminEducationException(400, "Video not found!");

            existVideo.ViewCount++;

            await repository.SaveChangesAsync();

            return existVideo.ViewCount;
        }

        public async Task<IEnumerable<string>> GetVideoLinksAsync(string playlistLink)
        {
            if (!playlistLink.Contains("list"))
                throw new ZaminEducationException(400, "Invalid Playlist Url");

            playlistLink = playlistLink.Split("list=")[1].Split("&")[0];

            var yt = new YoutubeClient();

            var videos = await yt.Playlists.GetVideosAsync(playlistLink);

            return videos.Select(p => p.Url);
        }

        private string YouTubeVideoIdExtractor(string link)
        {
            // Samples
            // https://www.youtube.com/watch?v=9Pv0Q8zFGP0&ab_channel=NajotTa%27lim
            // https://www.youtube.com/watch?v=5IanQIwhA4E

            if (!link.Contains("watch?"))
                throw new ZaminEducationException(400, "Invalid Youtube link");

            return link.Split("&")[0].Split('=')[1];
        }
    }
}
