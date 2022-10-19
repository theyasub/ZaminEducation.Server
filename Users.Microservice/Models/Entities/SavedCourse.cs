using Users.Microservice.Models.Commons;

namespace Users.Microservice.Models.Entities
{
    public class SavedCourse : Auditable
    {
        public long CourseId { get; set; }
        public Course Course { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}
