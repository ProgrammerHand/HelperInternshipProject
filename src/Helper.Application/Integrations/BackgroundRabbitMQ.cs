using Helper.Application.Abstraction.Events;
using Helper.Application.Integrations;
using Microsoft.Extensions.Hosting;

namespace Helper.Infrastructure.Integrations
{
    public class BackgroundRabbitMQ : IHostedService
    {
        private readonly RabbitMqClient client;

        public BackgroundRabbitMQ(IEventDispatcher eventDispatcher)
        {
            client = new RabbitMqClient(eventDispatcher);
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await client.CreateChannel();
            await client.CreateQueue("PaymentBus");
            while (true)
            {
                await client.ConsumeEventAsync();
            }

        }
        public Task StopAsync(CancellationToken cancellationToken) => client.DeleteChannel();
    }
}
