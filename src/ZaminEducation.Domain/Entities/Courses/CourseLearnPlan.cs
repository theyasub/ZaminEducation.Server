using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaminEducation.Domain.Commons;

namespace ZaminEducation.Domain.Entities.Courses
{
    public class CourseLearnPlan : Auditable<long>
    {
        public string Title { get; set; }
        public long CourseId { get; set; }
    }
}