
using ZaminEducation.Domain.Commons;

namespace ZaminEducation.Domain.Entities.Commons
{
    public class Attachment : Auditable
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}