
using ZaminEducation.Domain.Commons;
using ZaminEducation.Domain.Entities.Courses;

namespace ZaminEducation.Domain.Entities.Quizzes
{
    public class Quiz : Auditable
    {
        public long CourseId { get; set; }
        public Course Course { get; set; }

        public long QuestionId { get; set; }
        public QuizContent Question { get; set; }

        public virtual ICollection<QuestionAnswer> Answers { get; set; }
    }
}