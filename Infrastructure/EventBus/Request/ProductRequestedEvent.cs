using Infrastructure.EventBus.RabbitMQEventBus;

namespace Infrastructure.EventBus.Request
{
    public class ProductRequestedEvent: IntegrationEvent
    {
        public Guid ProductId { get; set; }

        public ProductRequestedEvent(Guid productId)
        {
            ProductId = productId;
        }
    }
}
