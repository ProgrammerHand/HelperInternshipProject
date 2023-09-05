using Helper.Application.Abstraction.Commands;
using Helper.Application.Exceptions;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.Offer;
using RabbitMQ.Client;
using System.Text;

namespace Helper.Application.Offer.Commands.Handlers
{
    public sealed class AcceptOfferHandler : ICommandHandler<AcceptOffer>
    {
        public readonly IOfferRepository _offerRepo;
        public AcceptOfferHandler(IOfferRepository offerRepo)
        {
            _offerRepo = offerRepo;
        }

        public async Task HandleAsync(AcceptOffer command)
        {
            var offer = await _offerRepo.GetByIdAsync(command.OfferId);
            offer.Accept();
            await _offerRepo.UpdateAsync(offer);
           
            
            
            
            
            
            
            //var factory = new ConnectionFactory() { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
            //    channel.QueueDeclare(queue: "PaymentBus",
            //        durable: false,
            //        exclusive: false,
            //        autoDelete: false,
            //        arguments: null);

            //   var body = Encoding.UTF8.GetBytes(offer.Id.Value.ToString());

            //   channel.BasicPublish(exchange: "",
            //       routingKey: "PaymentBus",
            //       basicProperties: null, body);
            //}
        }
    }
}
