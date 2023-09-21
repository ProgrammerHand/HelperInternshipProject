using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;
using Helper.Application.Offer.Queries;
using Helper.Core.Offer;
using Microsoft.EntityFrameworkCore;

namespace Helper.Infrastructure.DAL.Handlers
{
    internal class GetOfferHandler : IQueryHandler<GetOffer, OfferDto>
    {
        private readonly HelperDbContext _context;

        public GetOfferHandler(HelperDbContext DbContext) => _context = DbContext;
        public async Task<OfferDto> HandleAsync(GetOffer query)
        {
            var offerId = new OfferId(query.Id);
            var offer = await _context.Offers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == offerId);
            return offer?.AsDto();
        }
    }
}
