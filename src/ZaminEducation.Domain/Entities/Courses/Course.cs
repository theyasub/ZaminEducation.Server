using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZaminEducation.Domain.Commons;
using ZaminEducation.Domain.Entities.Commons;
using ZaminEducation.Domain.Entities.UserCourses;
using ZaminEducation.Domain.Entities.Users;
using ZaminEducation.Domain.Enums;

namespace ZaminEducation.Domain.Entities.Courses
{
    public class Course : Auditable
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public long AuthorId { get; set; }
        public User Author { get; set; }

        public int ViewCount { get; set; }

        public long ImageId { get; set; }
        public Attachment Image { get; set; }

        public long CategoryId { get; set; }
        public CourseCategory Category { get; set; }

        public CourseLevel Level { get; set; }

        public virtual ICollection<HashTag> HashTags { get; set; }
        public virtual ICollection<CourseModule> Modules { get; set; }
        public virtual ICollection<CourseTarget> Targets { get; set; }
        public virtual ICollection<CourseRate> Rates { get; set; }
    }
}