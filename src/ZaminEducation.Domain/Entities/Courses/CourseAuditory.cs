using ZaminEducation.Domain.Commons;

namespace ZaminEducation.Domain.Entities.Courses
{
    public class CourseAuditory : Auditable<long>
    {
        public string Name { get; set; }
    }
}