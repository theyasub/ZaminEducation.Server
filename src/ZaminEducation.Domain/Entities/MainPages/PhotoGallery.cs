using Newtonsoft.Json;
using ZaminEducation.Domain.Commons;
using ZaminEducation.Domain.Entities.Commons;
using ZaminEducation.Domain.Entities.MainPages.Commons;

namespace ZaminEducation.Domain.Entities.MainPages;

public class PhotoGallery : Text
{
    public virtual ICollection<PhotoGalleryAttachment> Photos { get; set; }

    [JsonIgnore]
    public HomePage HomePage { get; set; }
}