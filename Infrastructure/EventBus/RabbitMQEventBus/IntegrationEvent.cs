using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EventBus.RabbitMQEventBus
{
    public abstract class IntegrationEvent
    {
        public Guid Id { get; }
        public DateTime CreationDate { get; }

        protected IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
