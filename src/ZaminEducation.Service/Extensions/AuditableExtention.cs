using ZaminEducation.Domain.Commons;
using ZaminEducation.Domain.Enums;
using ZaminEducation.Service.Helpers;

namespace ZaminEducation.Service.Extensions
{
    public static class AuditableExtention
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
