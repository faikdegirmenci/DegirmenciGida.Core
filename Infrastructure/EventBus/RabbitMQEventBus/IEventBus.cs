using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EventBus.RabbitMQEventBus
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : IntegrationEvent;
        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>; // Her event için bir event handler
        void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
    }
}
