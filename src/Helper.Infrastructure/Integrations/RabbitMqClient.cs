﻿using Helper.Application.Abstraction.Events;
using Helper.Application.Integrations;
using Helper.Application.Offer.Events;
using Helper.Application.Solution.Events;
using Microsoft.Extensions.Configuration;
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
        private static IConnection? _connection;

        public RabbitMqClient(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            string adress = configuration.GetValue<string>("MessageBrockerAdress");
            if (adress.Contains("localhost"))
            {
                _factory = new ConnectionFactory()
                {
                    HostName = adress,
                    DispatchConsumersAsync = true
                };
            }
            else 
            {
                _factory = new ConnectionFactory()
                {
                    Uri =new Uri(adress),
                    DispatchConsumersAsync = true
                };
            }
            if(_connection is null)
                _connection = _factory.CreateConnection();
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
            using (var channel = _connection.CreateModel())
            {
                await CreateQueue(channel, routingKey);
                var body = Encoding.UTF8.GetBytes(data);
                channel.BasicPublish(exchange: "",
                routingKey: routingKey,
                basicProperties: null, body);
            }
        }

        public async Task ConsumeEventAsync(string queueName = "PaymentBus")
        {
            using (var channel = _connection.CreateModel())
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
                            await scope.ServiceProvider.GetRequiredService<IEventDispatcher>().PublishAsync(data);
                };
                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                await Task.Delay(1);
            }
            //await Task.Delay(100);
        }
    }
}
