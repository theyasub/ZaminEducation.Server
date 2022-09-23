using System.ComponentModel.DataAnnotations;

namespace ZaminEducation.Service.DTOs.CoursesDtos
{
    public class HashTagForCreationDto
    {
        [Required]
        public string Name { get; set; }
    }
}
