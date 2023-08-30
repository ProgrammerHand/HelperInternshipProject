﻿using Helper.Application.Abstraction.Events;
using Helper.Application.Integrations;
using Helper.Application.Offer.Events;
using Helper.Core.Offer;

namespace Helper.Application.Solution.Events.Handlers
{
    public class OfferPaidHandler : IEventHandler<OfferPaid>
    {
        private readonly IOfferRepository _offerRepo;
        private readonly IGoogleDriveClient _driveClient;

        public OfferPaidHandler(IOfferRepository offerRepo, IGoogleDriveClient driveClient)
        {
            _offerRepo = offerRepo;
            _driveClient = driveClient;
        }

        public async Task HandleAsync(OfferPaid @event)
        {
            var offer = await _offerRepo.GetByIdAsync(@event.Id);
            var solutionStorage = await _driveClient.CreateFolder(offer.Id.Value.ToString());
            offer.AddSolutionStorage(solutionStorage);
            await _offerRepo.UpdateAsync(offer);
        }
    }
}
