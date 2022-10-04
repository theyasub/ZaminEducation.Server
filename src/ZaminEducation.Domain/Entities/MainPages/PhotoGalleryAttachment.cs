using Newtonsoft.Json;
using ZaminEducation.Domain.Commons;
using ZaminEducation.Domain.Entities.Commons;

namespace ZaminEducation.Domain.Entities.MainPages
{
    public class PhotoGalleryAttachment : Auditable
    {
        [JsonIgnore]
        public long PhotoGalleryId { get; set; }
        public PhotoGallery PhotoGallery { get; set; }

        [JsonIgnore]
        public long AttachmentId { set; get; }
        public Attachment Attachment { set; get; }
    }
}