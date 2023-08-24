using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;
using Helper.Application.Inquiry.Queries;
using Helper.Application.Offer.Queries;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.Offer.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
