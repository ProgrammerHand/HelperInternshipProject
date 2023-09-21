using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;
using Helper.Core.User;

namespace Helper.Application.Offer.Queries
{
    public class GetOwnedOffers : IQuery<List<OfferDto>>
    {
        public UserId CustomerId { get; private set; }

        public GetOwnedOffers(Guid id)
        {
            CustomerId = id;
        }
    }
}
