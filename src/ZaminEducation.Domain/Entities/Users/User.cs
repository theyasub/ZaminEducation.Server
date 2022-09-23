using System.ComponentModel.DataAnnotations;
using ZaminEducation.Domain.Commons;
using ZaminEducation.Domain.Entities.Commons;
using ZaminEducation.Domain.Entities.UserCourses;
using ZaminEducation.Domain.Enums;

namespace ZaminEducation.Domain.Entities.Users
{
    public class User : Auditable
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, MinLength(5)]
        public string Password { get; set; }

        public long? AddressId { get; set; }
        public Address Address { get; set; }

        [MaxLength(200)]
        public string Bio { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public long? ImageId { get; set; }
        public Attachment Image { get; set; }

        public UserRole Role { get; set; } = UserRole.User;

        public DateTime? DateOfBirth { get; set; }

        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<SavedCourse> SavedCourses { get; set; }
    }
}