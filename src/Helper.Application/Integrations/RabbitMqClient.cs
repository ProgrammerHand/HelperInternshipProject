using Helper.Application.Abstraction.Events;
using Helper.Application.DTO;
using Helper.Application.Offer.Events;
using HelperPayment.Core.DTO;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Helper.Application.Integrations
{
    public class RabbitMqClient
    {
        private IConnection? _connection;
        private IModel? _channel;
        private readonly IEventDispatcher _eventDispatcher;

        public RabbitMqClient(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        public async Task CreateChannel()
        {
            var _factory = new ConnectionFactory()
            {
                HostName = "localhost",
                DispatchConsumersAsync = true
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public async Task CreateQueue(string name = "OfferBus")
        {
            _channel.QueueDeclare(queue: name,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
        }

        public async Task DeleteChannel()
        {
            if (_connection.IsOpen)
                _connection.Abort();
        }

        public async Task PublishEvent(string data, string routingKey = "OfferBus")
        {
            var body = Encoding.UTF8.GetBytes(data);
            _channel.BasicPublish(exchange: "",
                routingKey: routingKey,
                basicProperties: null, body);
        }

        public async Task ConsumeEventAsync()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            await Task.Yield();
            consumer.Received += async (model, ea) =>
            {
                var stream = new MemoryStream(ea.Body.ToArray());
                var data = await JsonSerializer.DeserializeAsync<InvoicePayment>(stream);
                if (data is InvoicePayment)
                    await _eventDispatcher.PublishAsync(new OfferPaid(data.OfferId));
            };
            _channel.BasicConsume(queue: "PaymentBus", autoAck: true, consumer: consumer);
        }
    }
}
