using System.ComponentModel.DataAnnotations;

namespace ZaminEducation.Service.DTOs.HomePage;

public class SocialNetworksForCreationDto
{
    [EmailAddress]
    public string EmailLink { get; set; }

    public string FacebookLink { get; set; }

    public string InstagrammLink { get; set; }
    public string TelegramLink { get; set; }

    [RegularExpression("^(https?\\:\\/\\/)?(www\\.youtube\\.com|youtu\\.be)\\/.+$")]
    public string YouTubeLink { get; set; }
}