using Helper.Application.Abstraction.Commands;
using Helper.Application.Abstraction.Events;
using Helper.Application.Exceptions;
using Helper.Application.Integrations;
using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.Offer;
using HelperPayment.Core.DTO;
using RabbitMQ.Client;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;

namespace Helper.Application.Offer.Commands.Handlers
{
    public sealed class AcceptOfferHandler : ICommandHandler<AcceptOffer>
    {
        public readonly IOfferRepository _offerRepo;
        public readonly IInquiryRepository _inquiryRepo;
        private readonly IEventDispatcher _eventDispatcher;

        public AcceptOfferHandler(IOfferRepository offerRepo, IInquiryRepository inquiryRepo, IEventDispatcher eventDispatcher)
        {
            _offerRepo = offerRepo;
            _eventDispatcher = eventDispatcher;
            _inquiryRepo = inquiryRepo;
        }

        public async Task HandleAsync(AcceptOffer command)
        {
            var offer = await _offerRepo.GetByIdAsync(command.OfferId);
            var inquiry = await _inquiryRepo.GetByIdAsync(offer.InquiryId);
            offer.Accept();
            await _offerRepo.UpdateAsync(offer);
            var client = new RabbitMqClient(_eventDispatcher);
            await client.CreateChannel();
            await client.CreateQueue();
            var dto = new InvoiceCreationDto()
            {
                OfferId = offer.Id.Value,
                PaymentDate = offer.PaymentDate.Value,
                RealisationStart = inquiry.RequestedCompletionDate.Start,
                RealisationEnd = (DateTime)inquiry.RequestedCompletionDate.End,
                Price = offer.Price,
                ClientEmail = inquiry.Author.Email.Value,
                ClientName = "User"
            };
            var serialized = JsonSerializer.Serialize(dto);
            await client.PublishEvent(serialized);
            await client.DeleteChannel();
        }
    }
}
