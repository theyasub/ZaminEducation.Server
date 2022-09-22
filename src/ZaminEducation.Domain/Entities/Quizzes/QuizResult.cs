using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaminEducation.Domain.Commons;
using ZaminEducation.Domain.Entities.Courses;
using ZaminEducation.Domain.Entities.Users;

namespace ZaminEducation.Domain.Entities.Quizzes
{
    public class QuizResult : Auditable
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public long CourseId { get; set; }
        public Course Course { get; set; }

        public double Percentage { get; set; }
    }
}