using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public abstract class BaseEntity<TId>: IEntityTimestamps
    {
        public TId Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }

        public void SetDeleted()
        {
            IsDeleted = true;
        }

        public void SetUpdatedDate()
        {
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
