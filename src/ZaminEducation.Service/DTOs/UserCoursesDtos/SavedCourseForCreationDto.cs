using ZaminEducation.Service.DTOs.Courses;
using ZaminEducation.Service.DTOs.Users;

namespace ZaminEducation.Service.DTOs.UserCourses
{
    public class SavedCourseForCreationDto
    {
        public long CourseId { get; set; }

        public long UserId { get; set; }
    }
}
