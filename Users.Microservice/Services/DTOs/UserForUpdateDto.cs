using System.ComponentModel.DataAnnotations;
using Users.Microservice.Models.Enums;

namespace Users.Microservice.Services.DTOs
{
    public class UserForUpdateDto
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [MaxLength(200)]
        public string Bio { get; set; }

        [Required]
        public Gender Gender { get; set; }
    }
}
