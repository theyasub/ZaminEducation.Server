using System.ComponentModel.DataAnnotations;

namespace ZaminEducation.Service.DTOs.Courses
{
    public class CourseModuleForCreationDto
    {
        [Required]
        public string Name { get; set; }
        public long CourseId { get; set; }
    }
}
