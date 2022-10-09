using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using ZaminEducation.Service.Extensions.Attributes;

namespace ZaminEducation.Service.DTOs.HomePage;

public class ImageForCreationDto
{
    [Required]
    [IsNoMoreThenMaxSize(3), FormFileAttributes]
    public IFormFile File { get; set; }
}