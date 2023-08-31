using Helper.Application.Abstraction.Events;
using Helper.Application.Inquiry.Events;
using Helper.Core.Inquiry;
using Helper.Core.Offer;
using Helper.Core.Solution;

namespace Helper.Application.Offer.Events.Handlers
{
    public class InquiryAcceptedHandler : IEventHandler<InquiryAccepted>
    {
        private readonly IInquiryRepository _inquiryRepo;
        private readonly IOfferRepository _offerRepo;
        //private readonly ISolutionRepository _solutionRepo;

        public InquiryAcceptedHandler(IInquiryRepository inquiryRepo, IOfferRepository offerRepo)//, ISolutionRepository solutionRepo)
        {
            _inquiryRepo = inquiryRepo;
            _offerRepo = offerRepo;
            //_solutionRepo = solutionRepo;
        }

        public async Task HandleAsync(InquiryAccepted @event)
        {
            var inquiry = await _inquiryRepo.GetByIdAsync(@event.inquiryId);
            var offer = Core.Offer.Offer.CreateOffer(inquiry);
            await _offerRepo.AddAsync(offer);
            //var solution = Core.Solution.Solution.CreateSolution(inquiry);
            //await _solutionRepo.AddAsync(solution);
        }
    }
}
