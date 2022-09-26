using ZaminEducation.Domain.Commons;

namespace ZaminEducation.Domain.Entities.Courses
{
    public class YouTubeVideo : Auditable
    {
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public long Length { get; set; }
        public long ViewCount { get; set; }
        public long CourseId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
