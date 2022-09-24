using ZaminEducation.Service.DTOs.CoursesDtos;
using ZaminEducation.Service.DTOs.UsersDtos;

namespace ZaminEducation.Service.DTOs.UserCoursesDtos
{
    public class CourseRateForCreationDto
    {
        public UserForCreationDto User { get; set; }

        public CourseForCreationDto Course { get; set; }

        public byte Value { get; set; }
    }
}
