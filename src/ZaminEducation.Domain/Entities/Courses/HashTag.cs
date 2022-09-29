
using System.ComponentModel.DataAnnotations.Schema;
using ZaminEducation.Domain.Commons;

namespace ZaminEducation.Domain.Entities.Courses
{
    public class HashTag : Auditable
    {
        public string Name { get; set; }

        public long CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }
    }
}