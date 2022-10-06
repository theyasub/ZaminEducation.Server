using Newtonsoft.Json;
using ZaminEducation.Domain.Entities.Commons;
using ZaminEducation.Domain.Entities.MainPages.Commons;

namespace ZaminEducation.Domain.Entities.MainPages;

public class HomePageHeader : Text
{
    public Image Image { get; set; }

    public string YouTubeVideoLink { get; set; }
    
}