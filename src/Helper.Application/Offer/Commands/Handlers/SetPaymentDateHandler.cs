﻿using Helper.Application.Abstraction.Commands;
using Helper.Core.Offer;

namespace Helper.Application.Offer.Commands.Handlers
{
    public sealed class SetPaymentDateHandler : ICommandHandler<SetOfferPaymentDate>
    {
        private IOfferRepository _offerRepo;

        public SetPaymentDateHandler(IOfferRepository offerRepo)
        {
            _offerRepo = offerRepo;
        }
        public async Task HandleAsync(SetOfferPaymentDate command)
        {
            var entity = await _offerRepo.GetByIdAsync(command.OfferId);
            entity.AddPaymentDate(command.PaymentDate);
            await _offerRepo.UpdateAsync(entity);
        }
    }
}
