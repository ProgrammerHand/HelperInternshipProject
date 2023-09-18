using Helper.Application.Abstraction.Commands;
using Helper.Application.Integrations;
using Helper.Application.Offer.Events;
using Helper.Core.Inquiry;
using Helper.Core.Offer;
using System.Text.Json;

namespace Helper.Application.Offer.Commands.Handlers
{
    public sealed class AcceptOfferHandler : ICommandHandler<AcceptOffer>
    {
        public readonly IOfferRepository _offerRepo;
        public readonly IInquiryRepository _inquiryRepo;
        private readonly IRabbitMqClient client;

        public AcceptOfferHandler(IOfferRepository offerRepo, IRabbitMqClient rabbitMqClien,
           IInquiryRepository inquiryRepo)
        {
            _offerRepo = offerRepo;
            _inquiryRepo = inquiryRepo;
            client = rabbitMqClien;
        }

        public async Task HandleAsync(AcceptOffer command)
        {
            var offer = await _offerRepo.GetByIdAsync(command.OfferId);
            var inquiry = await _inquiryRepo.GetByIdAsync(offer.InquiryId);
            offer.Accept();
            var dto = new InvoiceCreatedEvent()
            {
                OfferId = offer.Id.Value,
                PaymentDate = offer.PaymentDate.Value,
                RealisationStart = inquiry.RequestedCompletionDate.Start,
                RealisationEnd = (inquiry.RequestedCompletionDate.End is null)? null : (DateTime)inquiry.RequestedCompletionDate.End,
                Price = offer.Price,
                ClientEmail = inquiry.Author.Email.Value,
                ClientName = "User"
            };
            await _offerRepo.UpdateAsync(offer);
            var serialized = JsonSerializer.Serialize(dto);
            await client.PublishEvent(serialized, "OfferBus");
        }
    }
}
