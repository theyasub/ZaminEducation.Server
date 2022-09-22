using System.Net.Mail;
using ZaminEducation.Domain.Commons;

namespace ZaminEducation.Domain.Entities.Quizzes
{
    public class QuizAsset : Auditable
    {
        public long QuizContentId { get; set; }
        public QuizContent Content { get; set; }

        public long FileId { get; set; }
        public Attachment File { get; set; }
    }
}