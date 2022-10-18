using Users.Microservice.Models.Commons;

namespace Users.Microservice.Models.Entities
{
    public class CourseCategory : Auditable
    {
        public string Name { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
