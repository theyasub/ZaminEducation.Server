using ZaminEducation.Domain.Entities.MainPages.Commons;

namespace ZaminEducation.Domain.Entities.MainPages;

public class PhotoGallery : Text
{
    public virtual IList<Image> Photos { get; set; }
}