using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;

namespace Helper.Application.Offer.Queries
{
    internal class GetOffer : IQuery<InquiryDto>
    {
        public Guid Id { get; private set; }

        public GetOffer(Guid id)
        {
            Id = id;
        }
    {
    }
}