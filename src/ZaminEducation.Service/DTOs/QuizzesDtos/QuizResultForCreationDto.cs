using ZaminEducation.Service.DTOs.Courses;
using ZaminEducation.Service.DTOs.Users;

namespace ZaminEducation.Service.DTOs.Quizzes
{
    public class QuizResultForCreationDto
    {
        public long UserId { get; set; }

        public long CourseId { get; set; }

        public double Percentage { get; set; }
    }
}
