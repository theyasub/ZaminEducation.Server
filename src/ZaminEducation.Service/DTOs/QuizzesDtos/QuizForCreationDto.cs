using ZaminEducation.Service.DTOs.Courses;

namespace ZaminEducation.Service.DTOs.Quizzes
{
    public class QuizForCreationDto
    {
        public long CourseId { get; set; }

        public long QuestionId { get; set; }
    }
}
