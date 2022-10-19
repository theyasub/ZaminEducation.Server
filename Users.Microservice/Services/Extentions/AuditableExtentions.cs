using Users.Microservice.Models.Commons;
using Users.Microservice.Models.Enums;
using Users.Microservice.Services.Helpers;

namespace Users.Microservice.Services.Extentions
{
    public static class AuditableExtentions
    {
        public static void Create(this Auditable auditable)
        {
            auditable.State = ItemState.Created;
            auditable.CreatedAt = DateTime.UtcNow;
            auditable.CreatedBy = HttpContextHelper.UserId;
        }

        public static void Update(this Auditable auditable)
        {
            auditable.State = ItemState.Updated;
            auditable.UpdatedAt = DateTime.UtcNow;
            auditable.UpdatedBy = HttpContextHelper.UserId;
        }
    }
}
