using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZaminEducation.Domain.Commons
{
    public abstract class Auditable<T>
    {
        public T Id {get; set;}
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public T UpdaterId { get; set; }

        public void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}