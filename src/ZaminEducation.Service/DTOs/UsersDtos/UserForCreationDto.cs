using System.ComponentModel.DataAnnotations;
using ZaminEducation.Domain.Enums;

namespace ZaminEducation.Service.DTOs.Users
{
    public class UserForCreationDto
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, MinLength(5)]
        public string Password { get; set; }

        public long AddressId { get; set; }

        [MaxLength(200)]
        public string Bio { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public long ImageId { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.User;

        public DateTime? DateOfBirth { get; set; }
    }
}
