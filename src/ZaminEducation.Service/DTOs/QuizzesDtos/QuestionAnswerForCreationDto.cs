namespace ZaminEducation.Service.DTOs.Quizzes
{
    public class QuestionAnswerForCreationDto
    {
        public long ContentId { get; set; }

        public bool IsCorrect { get; set; }

        public long QuizId { get; set; }

        public long QuestionId { get; set; }
    }
}
