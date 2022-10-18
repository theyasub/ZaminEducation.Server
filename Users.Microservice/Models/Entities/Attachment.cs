using Users.Microservice.Models.Commons;

namespace Users.Microservice.Models.Entities
{
    public class Attachment : Auditable
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
