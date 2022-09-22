using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaminEducation.Domain.Enums;

namespace ZaminEducation.Domain.Commons
{
    public abstract class Auditable
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public long? CreatedBy { get; set; }
        public ItemState State { get; set; } = ItemState.Created;
    }
}