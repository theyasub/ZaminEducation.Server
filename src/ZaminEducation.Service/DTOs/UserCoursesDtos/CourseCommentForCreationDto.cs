using ZaminEducation.Service.DTOs.CoursesDtos;
using ZaminEducation.Service.DTOs.UsersDtos;

namespace ZaminEducation.Service.DTOs.UserCoursesDtos
{
    public class CourseCommentForCreationDto
    {
        public string Text { get; set; }

        public UserForCreationDto User { get; set; }

        public CourseForCreationDto Course { get; set; }

        public CourseCommentForCreationDto Parent { get; set; }
    }
}