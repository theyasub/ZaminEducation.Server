using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using ZaminEducation.Domain.Entities.MainPages.Commons;

namespace ZaminEducation.Service.DTOs.HomePage;

public class OfferedOpportunitesForCreationDto
{
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }

    public IFormFile FormFile { get; set; }
}