
using ZaminEducation.Domain.Entities.Commons;
using ZaminEducation.Domain.Entities.Courses;
using ZaminEducation.Domain.Entities.Users;
using ZaminEducation.Domain.Enums;

namespace ZaminEducation.Service.ViewModels
{
    public class CourseViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User Author { get; set; }
        public int ViewCount { get; set; }
        public Attachment Image { get; set; }
        public CourseCategory Category { get; set; }
        public double Rate { get; set; }
        public CourseLevel Level { get; set; }

        public IEnumerable<CourseModule> Modules { get; set; }
        public IEnumerable<CourseTarget> Targets { get; set; }
        public IEnumerable<CourseVideo> Videos { get; set; }
    }
}
