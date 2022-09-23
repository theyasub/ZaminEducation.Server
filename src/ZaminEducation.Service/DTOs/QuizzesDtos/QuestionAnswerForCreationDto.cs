namespace ZaminEducation.Service.DTOs.QuizzesDtos
{
    public class QuestionAnswerForCreationDto
    {
        public QuizContentForCreationDto Content { get; set; }

        public bool IsCorrect { get; set; }

        public QuizForCreationDto Quiz { get; set; }

        public QuizContentForCreationDto Question { get; set; }
    }
}
