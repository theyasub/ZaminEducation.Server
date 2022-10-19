using Users.Microservice.Models.Commons;

namespace Users.Microservice.Models.Entities
{
    public class Region : Auditable
    {
        public string Name { get; set; }

        public long? ParentId { get; set; }
        public Region Parent { get; set; }
    }
}
