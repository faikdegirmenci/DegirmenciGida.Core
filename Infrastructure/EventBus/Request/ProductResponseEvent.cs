using Infrastructure.EventBus.RabbitMQEventBus;

namespace Infrastructure.EventBus.Request
{
    public class ProductResponseEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ProductResponseEvent(Guid productId, string name, decimal price)
        {
            Id = productId;
            Name = name;
            Price = price;
        }
    }
}
