using System.ComponentModel.DataAnnotations;
using ZaminEducation.Domain.Entities.MainPages.Commons;

namespace ZaminEducation.Service.DTOs.HomePage;

public class ReasonForCreationDto
{
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
}