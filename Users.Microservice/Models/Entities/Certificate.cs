using System.ComponentModel.DataAnnotations.Schema;
using Users.Microservice.Models.Commons;

namespace Users.Microservice.Models.Entities
{
    public class Certificate : Auditable
    {
        public long CourseId { get; set; }
        public Course Course { get; set; }

        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public long ImageId { get; set; }
        [ForeignKey(nameof(ImageId))]
        public Attachment Image { get; set; }
    }
}
