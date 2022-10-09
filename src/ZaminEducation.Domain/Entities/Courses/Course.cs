
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZaminEducation.Domain.Commons;
using ZaminEducation.Domain.Entities.Commons;
using ZaminEducation.Domain.Entities.Quizzes;
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
        public string YouTubePlaylistLink { get; set; }

        public long AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; }

        public int ViewCount { get; set; }

        public long? ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public Attachment Image { get; set; }

        public long CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public CourseCategory Category { get; set; }

        public CourseLevel Level { get; set; }

        public virtual ICollection<HashTag> HashTags { get; set; }
        public virtual ICollection<CourseModule> Modules { get; set; }
        public virtual ICollection<CourseTarget> Targets { get; set; }
        public virtual ICollection<CourseRate> Rates { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<CourseVideo> Videos { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}