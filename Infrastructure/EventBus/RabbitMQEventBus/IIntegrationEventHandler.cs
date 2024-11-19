using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EventBus.RabbitMQEventBus
{
    public interface IIntegrationEventHandler<TEvent> where TEvent : IntegrationEvent
    {
        Task Handle(TEvent @event);  // Event'in nasıl işleneceği bu metot ile tanımlanır.
    }
}
