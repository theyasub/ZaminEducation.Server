using Newtonsoft.Json;
using ZaminEducation.Domain.Entities.Commons;
using ZaminEducation.Domain.Entities.MainPages.Commons;

namespace ZaminEducation.Domain.Entities.MainPages;

public class HomePageHeader : Text
{
    [JsonIgnore]
    public long ImageId { get; set; }
    public Attachment Image { get; set; }

    public string YouTubeVideoLink { get; set; }

    [JsonIgnore]
    public HomePage HomePage { get; set; }
}