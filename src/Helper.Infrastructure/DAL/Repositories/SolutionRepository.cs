using Helper.Core.Offer.ValueObjects;
using Helper.Core.Offer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.Core.Solution;

namespace Helper.Infrastructure.DAL.Repositories
{
    public class SolutionRepository : ISolutionRepository
    {
        private readonly HelperDbContext _context;
        public SolutionRepository(HelperDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Solution> GetByIdAsync(SolutionId id)
        {
            return await _context.Solutions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Solution solution)
        {
            await _context.Solutions.AddAsync(solution);
            await Save();
        }

        public async Task UpdateAsync(Solution solution)
        {
            _context.Solutions.Update(solution);
            await Save();
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
