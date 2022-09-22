using ZaminEducation.Domain.Commons;

namespace ZaminEducation.Domain.Entities.Quizzes
{
    public class QuestionAnswer : Auditable
    {
        public long ContentId { get; set; }
        public QuizContent Content { get; set; }

        public bool IsCorrect { get; set; }

        public long QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public long QuestionId { get; set; }
        public QuizContent Question { get; set; }
    }
}