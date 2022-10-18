using Users.Microservice.Models.Enums;

namespace Users.Microservice.Models.Commons
{
    public class Auditable
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public long? CreatedBy { get; set; }
        public ItemState State { get; set; } = ItemState.Created;
    }
}
