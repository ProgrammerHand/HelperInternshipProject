using Helper.Core.Offer;
using Helper.Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Infrastructure.DAL.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly HelperDbContext _context;
        public OfferRepository(HelperDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task AddAsync(Offer offer)
        {
            await _context.Offers.AddAsync(offer);
            await Save();
        }

        private async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }
    }
}
