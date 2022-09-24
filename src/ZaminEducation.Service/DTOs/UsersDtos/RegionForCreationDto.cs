using System.ComponentModel.DataAnnotations;

namespace ZaminEducation.Service.DTOs.UsersDtos
{
    public class RegionForCreationDto
    {
        [MaxLength(50), Required]
        public string Name { get; set; }

        public RegionForCreationDto Parent { get; set; }
    }
}
