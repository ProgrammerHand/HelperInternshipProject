using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;

namespace Helper.Application.Offer.Queries
{
    public class GetOffer : IQuery<OfferDto>
    {
        public Guid Id { get; private set; }

        public GetOffer(Guid id)
        {
            Id = id;
        }
    }
}