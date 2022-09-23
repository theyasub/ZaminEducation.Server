using System.ComponentModel.DataAnnotations;
using ZaminEducation.Domain.Enums;
using ZaminEducation.Service.DTOs.Commons;
using ZaminEducation.Service.DTOs.UsersDtos;

namespace ZaminEducation.Service.DTOs.CoursesDtos
{
    public class CourseForCreationDto
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public UserForCreationDto Author { get; set; }

        public int ViewCount { get; set; }

        public AttachmentForCreationDto Image { get; set; }

        public CourseCategoryForCreationDto Category { get; set; }

        [Required]
        public CourseLevel Level { get; set; }
    }
}