using System.Collections.Generic;
using ZaminEducation.Domain.Commons;

namespace ZaminEducation.Domain.Entities.Courses
{
    public class CourseModule : Auditable<long>
    {
        public string Name { get; set; }
        public long CourseId { get; set; }
        public virtual ICollection<CourseVideo> Videos { get; set; }
    }
}