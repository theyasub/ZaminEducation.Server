using ZaminEducation.Domain.Commons;

namespace ZaminEducation.Domain.Entities.Courses
{
    public class CourseVideo : Auditable<long>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long Length { get; set; }
        public long ViewCount { get; set; }
        public long CourseId { get; set; }
        public long CourseModuleId { get; set; }
        public string Url { get; set; }
    }
}