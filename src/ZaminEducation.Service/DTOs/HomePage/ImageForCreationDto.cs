using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ZaminEducation.Service.DTOs.HomePage;

public class ImageForCreationDto
{
    [Required]
    public IFormFile File { get; set; }
}