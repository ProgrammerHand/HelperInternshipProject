using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;
using Helper.Application.Offer.Queries;
using Microsoft.EntityFrameworkCore;

namespace Helper.Infrastructure.DAL.Handlers
{
    internal class GetOwnedOffersHandler : IQueryHandler<GetOwnedOffers, List<OfferDto>>
    {
        private readonly HelperDbContext _context;
        public GetOwnedOffersHandler(HelperDbContext DbContext) => _context = DbContext;
        public async Task<List<OfferDto>> HandleAsync(GetOwnedOffers querry)
        {
            var entities = await _context.Offers.Where(x => x.CustomerId == querry.CustomerId).AsNoTracking().ToListAsync();
            var offers = new List<OfferDto>();
            foreach (var entity in entities)
            {
                offers.Add(entity.AsDto());
            }
            return offers;
        }
    }
}
