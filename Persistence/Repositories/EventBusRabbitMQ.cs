using Infrastructure.EventBus.RabbitMQEventBus;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Persistence.Repositories
{
    public class EventBusRabbitMQ : IEventBus
    {
        private readonly IConnection _connection;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly IServiceProvider _serviceProvider; // Dependency Injection için

        public EventBusRabbitMQ(IConnection connection, IServiceProvider serviceProvider)
        {
            _connection = connection;
            _handlers = new Dictionary<string, List<Type>>();
            _serviceProvider = serviceProvider;
        }

        public void Publish<T>(T @event) where T : IntegrationEvent
        {
            using (var channel = _connection.CreateModel())
            {
                var eventName = typeof(T).Name;
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));

                channel.BasicPublish(
                    exchange: "event_bus",
                    routingKey: eventName,
                    basicProperties: null,
                    body: body);
            }
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;

            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: eventName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var eventType = typeof(T);

                    var @event = JsonConvert.DeserializeObject(message, eventType) as T;

                    if (@event != null)
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var handler = (TH)scope.ServiceProvider.GetRequiredService(typeof(TH));
                            await handler.Handle(@event);
                        }
                    }
                };

                channel.BasicConsume(queue: eventName, autoAck: true, consumer: consumer);
            }
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {   
            var eventName = typeof(T).Name;

            if (_handlers.ContainsKey(eventName))
            {
                var handlerToRemove = _handlers[eventName].FirstOrDefault(handler => handler == typeof(TH));
                if (handlerToRemove != null)
                {
                    _handlers[eventName].Remove(handlerToRemove);
                }

                if (!_handlers[eventName].Any())
                {
                    _handlers.Remove(eventName);
                }
            }
        }
    }
}
