using System.ComponentModel.DataAnnotations;

namespace Users.Microservice.Services.DTOs
{
    public class AttachmentForCreationDto
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public Stream Stream { get; set; }
    }
}
