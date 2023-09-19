using Helper.Core.Solution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Helper.Infrastructure.DAL.Repositories
{
    public class SolutionRepository : ISolutionRepository
    {
        private readonly HelperDbContext _context;
        public SolutionRepository(HelperDbContext dbContext, IServiceProvider serviceProvider)
        {
            _context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<HelperDbContext>(); ;
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
