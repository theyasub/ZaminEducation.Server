using System.ComponentModel.DataAnnotations;

namespace ZaminEducation.Service.DTOs.Courses
{
    public class CourseCategoryForCreationDto
    {
        [Required]
        public string Name { get; set; }
    }
}
