using ZaminEducation.Service.DTOs.Courses;
using ZaminEducation.Service.DTOs.Users;

namespace ZaminEducation.Service.DTOs.UserCourses
{
    public class CourseRateForCreationDto
    {
        public long UserId { get; set; }

        public long CourseId { get; set; }

        public byte Value { get; set; }
    }
}
