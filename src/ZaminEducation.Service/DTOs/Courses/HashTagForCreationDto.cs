using System.ComponentModel.DataAnnotations;

namespace ZaminEducation.Service.DTOs.Courses
{
    public class HashTagForCreationDto
    {
        [Required]
        public string Name { get; set; }
    }
}
