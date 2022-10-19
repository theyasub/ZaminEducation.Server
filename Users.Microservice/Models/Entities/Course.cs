using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Users.Microservice.Models.Commons;
using Users.Microservice.Models.Enums;

namespace Users.Microservice.Models.Entities
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
    }
}
