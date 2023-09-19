using Helper.Core.ReservedEmployeeTime;
using Microsoft.EntityFrameworkCore;

namespace Helper.Infrastructure.DAL.Repositories
{
    public class ReservedEmployeeTimeRepository : IReservedEmployeeTimeRepository
    {
        private readonly HelperDbContext _context;
        public ReservedEmployeeTimeRepository(HelperDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<ReservedEmployeeTime> GetByIdAsync(ReservedEmployeeTimeId id)
        {
            return await _context.ReservedEmployeeTime.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(ReservedEmployeeTime reservation)
        {
            await _context.ReservedEmployeeTime.AddAsync(reservation);
            await Save();
        }

        public async Task UpdateAsync(ReservedEmployeeTime reservation)
        {
            _context.ReservedEmployeeTime.Update(reservation);
            await Save();
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
