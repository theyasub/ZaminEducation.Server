using System.ComponentModel.DataAnnotations;

namespace ZaminEducation.Service.DTOs.CoursesDtos
{
    public class CourseCategoryForCreationDto
    {
        [Required]
        public string Name { get; set; }
    }
}
