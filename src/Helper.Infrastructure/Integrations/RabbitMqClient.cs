using Helper.Application.Abstraction.Events;
using Helper.Application.Integrations;
using Helper.Application.Offer.Events;
using Helper.Application.Solution.Events;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Helper.Infrastructure.Integrations
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConnectionFactory _factory;

        public RabbitMqClient(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _factory = new ConnectionFactory()
            {
                HostName = "localhost",
                DispatchConsumersAsync = true
            };
        }

        internal async Task CreateQueue(IModel channel, string name = "OfferBus")
        {
            channel.QueueDeclare(queue: name,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
        }

        public async Task PublishEvent(string data, string routingKey = "OfferBus")
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    await CreateQueue(channel, routingKey);
                    var body = Encoding.UTF8.GetBytes(data);
                    channel.BasicPublish(exchange: "",
                    routingKey: routingKey,
                    basicProperties: null, body);
                }
            }
        }

        public async Task ConsumeEventAsync(string queueName = "PaymentBus")
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    await CreateQueue(channel, queueName);
                    var consumer = new AsyncEventingBasicConsumer(channel);
                    await Task.Yield();
                    consumer.Received += async (model, ea) =>
                    {
                        var stream = new MemoryStream(ea.Body.ToArray());
                        var data = await JsonSerializer.DeserializeAsync<InvoicePaidEvent>(stream);
                        if (data is InvoicePaidEvent)
                            using (var scope = _serviceProvider.CreateScope())
                                await scope.ServiceProvider.GetRequiredService<IEventDispatcher>().PublishAsync(new OfferPaid(data.OfferId));
                    };
                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                    await Task.Delay(1);
                }
            }
            //await Task.Delay(100);
        }
    }
}
