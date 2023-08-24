using Helper.Core.Offer.ValueObjects;

namespace Helper.Core.Offer
{
    public interface IOfferRepository
    {
        Task AddAsync(Offer offer);
        Task UpdateAsync(Offer offer);
        Task<Offer> GetByIdAsync(OfferId offer);
    }
}
