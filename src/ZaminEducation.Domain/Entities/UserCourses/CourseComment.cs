
using ZaminEducation.Domain.Commons;
using ZaminEducation.Domain.Entities.Courses;
using ZaminEducation.Domain.Entities.Users;

namespace ZaminEducation.Domain.Entities.UserCourses
{
    public class CourseComment : Auditable
    {
        public string Text { get; set; }

        public bool IsReplied { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }

        public long CourseId { get; set; }
        public Course Course { get; set; }

        public long? ParentId { get; set; }
        public CourseComment Parent { get; set; }
    }
}