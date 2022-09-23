using ZaminEducation.Service.DTOs.CoursesDtos;

namespace ZaminEducation.Service.DTOs.QuizzesDtos
{
    public class QuizForCreationDto
    {
        public CourseForCreationDto Course { get; set; }

        public QuizContentForCreationDto Question { get; set; }
    }
}
