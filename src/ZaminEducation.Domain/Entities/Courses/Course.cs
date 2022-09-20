using System.Net.Mail;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZaminEducation.Domain.Commons;
using System.Collections.Generic;

namespace ZaminEducation.Domain.Entities.Courses
{
    public class Course : Auditable<long>
    {
        [MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(300)]
        public string Description { get; set; }

        public int ViewCount { get; set; }

        [MaxLength(50)]
        public string Author { get; set; }

        public long TypeID { get; set; }

        [ForeignKey(nameof(TypeID))]
        public CourseType Type { get; set;}

        public long? ImageId { get; set; }

        [ForeignKey(nameof(ImageId))] 
        public Attachment Image { get; set; }

        public long AuditoryId { get; set; }

        [ForeignKey(nameof(AuditoryId))]
        public CourseAuditory Auditory { get; set; }

        public int StarValue { get; set; }

        public virtual ICollection<CourseLearnPlan> LearnPlans { get; set; } 
        public virtual ICollection<CourseModule> Modules { get; set; }
    }
}