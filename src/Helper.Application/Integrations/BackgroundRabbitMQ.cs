using Helper.Application.Integrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Helper.Infrastructure.Integrations
{
    public class BackgroundRabbitMQ : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public BackgroundRabbitMQ(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            while (true)
            {
                var client = scope.ServiceProvider.GetRequiredService<IRabbitMqClient>();
                await client.ConsumeEventAsync();
                //await Task.Yield();
            }
        }
    }
}
