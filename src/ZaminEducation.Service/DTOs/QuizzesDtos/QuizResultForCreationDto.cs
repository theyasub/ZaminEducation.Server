using ZaminEducation.Service.DTOs.CoursesDtos;
using ZaminEducation.Service.DTOs.UsersDtos;

namespace ZaminEducation.Service.DTOs.QuizzesDtos
{
    public class QuizResultForCreationDto
    {
        public UserForCreationDto User { get; set; }

        public CourseForCreationDto Course { get; set; }

        public double Percentage { get; set; }
    }
}
