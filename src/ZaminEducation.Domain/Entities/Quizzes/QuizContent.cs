using ZaminEducation.Domain.Commons;

namespace ZaminEducation.Domain.Entities.Quizzes
{
    public class QuizContent : Auditable
    {
        public string Text { get; set; }
        public virtual ICollection<QuizAsset> Assets { get; set; }
    }
}