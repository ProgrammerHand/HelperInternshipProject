namespace Helper.Core.Offer
{
    public interface IOfferRepository
    {
        Task AddAsync(Offer offer);
    }
}
