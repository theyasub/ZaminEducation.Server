using System.ComponentModel.DataAnnotations;

namespace ZaminEducation.Service.DTOs.CoursesDtos
{
    public class CourseModuleForCreationDto
    {
        [Required]
        public string Name { get; set; }
        public CourseForCreationDto Course { get; set; }
    }
}
