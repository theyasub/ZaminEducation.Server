namespace ZaminEducation.Service.DTOs.Quizzes
{
    public class QuizResultForCreationDto
    {
        public long UserId { get; set; }

        public long CourseId { get; set; }

        public double Percentage { get; set; }
    }
}
