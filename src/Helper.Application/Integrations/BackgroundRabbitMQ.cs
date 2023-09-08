using Helper.Application.Abstraction.Events;
using Helper.Application.Integrations;
using Microsoft.Extensions.Hosting;

namespace Helper.Infrastructure.Integrations
{
    public class BackgroundRabbitMQ : BackgroundService
    {
        private readonly RabbitMqClient client;

        public BackgroundRabbitMQ(IEventDispatcher eventDispatcher)
        {
            client = new RabbitMqClient(eventDispatcher);
        }


        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await client.CreateChannel();
            await client.CreateQueue("PaymentBus");
            while (true)
            {
                await client.ConsumeEventAsync();
                await Task.Yield();
            }
        }
    }
}
