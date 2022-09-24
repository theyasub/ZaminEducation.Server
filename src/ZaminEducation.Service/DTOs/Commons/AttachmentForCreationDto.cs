using System.ComponentModel.DataAnnotations;

namespace ZaminEducation.Service.DTOs.Commons
{
    public class AttachmentForCreationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }
    }
}
