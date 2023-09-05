using Helper.Core.Offer;
using Helper.Core.User;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Offer> GetByIdAsync(OfferId id) 
        {
            return await _context.Offers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Offer offer)
        {
            await _context.Offers.AddAsync(offer);
            await Save();
        }

        public async Task UpdateAsync(Offer offer)
        {
            _context.Offers.Update(offer);
            await Save();
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
