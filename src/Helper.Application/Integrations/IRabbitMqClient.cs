using Helper.Application.Abstraction.Events;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;

namespace Helper.Application.Integrations
{
    public interface IRabbitMqClient
    {
        public Task CreateChannel();
        public Task CreateQueue(string name = "OfferBus");
        public Task DeleteChannel();
        public Task PublishEvent(string data, string routingKey = "OfferBus");
        public Task ConsumeEventAsync();
    }
}
