using Helper.Core.Inquiry;
using Helper.Core.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace Helper.Infrastructure.DAL.Repositories
{
    public sealed class InquiryRepository : IInquiryRepository
    {
        private readonly HelperDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly DbContextOptions<HelperDbContext> _options;
        private readonly IClockCustom _clock;

        public InquiryRepository(HelperDbContext dbContext, IServiceProvider serviceProvider)
        {
            _context = dbContext;
            _serviceProvider = serviceProvider;
        }

        public async Task AddAsync(Inquiry inquiry)
        {
            await _context.Inquiries.AddAsync(inquiry);
            await Save();
        }

        public async Task<Inquiry> GetByIdAsync(InquiryId id) 
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<HelperDbContext>();
                return await context.Inquiries.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == id);
            }
           
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
