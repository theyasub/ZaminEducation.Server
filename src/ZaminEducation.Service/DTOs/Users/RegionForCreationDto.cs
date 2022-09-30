using System.ComponentModel.DataAnnotations;

namespace ZaminEducation.Service.DTOs.Users
{
    public class RegionForCreationDto
    {
        [MaxLength(50), Required]
        public string Name { get; set; }
        public long ParentId { get; set; }
    }
}
