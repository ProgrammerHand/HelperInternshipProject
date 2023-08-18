using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Helper.Infrastructure.DAL.Repositories
{
    internal sealed class InquiryRepository : IInquiryRepository
    {
        private readonly HelperDbContext _context;

        public InquiryRepository(HelperDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task AddAsync(Inquiry inquiry)
        {
            await _context.Inquiries.AddAsync(inquiry);
            await Save();
        }

        public async Task<Inquiry> GetByIdAsync(InquiryId id) 
        {
            return await _context.Inquiries.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Inquiry inquiry)
        {
            _context.Update(inquiry);
            await Save();
        }

        private async Task Save()
        {
            var saved = _context.SaveChangesAsync();
        }
    }
}
