namespace Helper.Application.Integrations
{
    public interface IRabbitMqClient
    {
        public Task PublishEvent(string data, string routingKey = "OfferBus");
        public Task ConsumeEventAsync(string queueName = "PaymentBus");
    }
}
