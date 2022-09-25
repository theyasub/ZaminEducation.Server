using System.ComponentModel.DataAnnotations;

namespace ZaminEducation.Service.DTOs.Commons
{
    public class AttachmentForCreationDto
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public Stream Stream { get; set; }
    }
}
