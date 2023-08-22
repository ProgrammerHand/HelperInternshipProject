using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;

namespace Helper.Infrastructure.DAL.Repositories
{
    public sealed class InquiryRepository : IInquiryRepository
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
            return await _context.Inquiries.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteInquiry(Inquiry inquiry)
        {
            _context.ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
            _context.Inquiries.Remove(inquiry);
            await Save();
        }


        public async Task UpdateAsync(Inquiry inquiry)
        {
            _context.Inquiries.Update(inquiry);
            await Save();
        }
 

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
