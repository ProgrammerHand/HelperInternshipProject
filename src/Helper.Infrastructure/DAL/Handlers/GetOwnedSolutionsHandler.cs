using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;
using Helper.Application.Solution.Queries;
using Microsoft.EntityFrameworkCore;

namespace Helper.Infrastructure.DAL.Handlers
{
    internal class GetOwnedSolutionsHandler : IQueryHandler<GetOwnedSolutions, List<SolutionDto>>
    {
        private readonly HelperDbContext _context;
        public GetOwnedSolutionsHandler(HelperDbContext DbContext) => _context = DbContext;
        public async Task<List<SolutionDto>> HandleAsync(GetOwnedSolutions querry)
        {
            var entities = await _context.Solutions.Where(x => x.AssignedConsultant == querry.WorkerId).AsNoTracking().ToListAsync();
            var solutions = new List<SolutionDto>();
            foreach (var solution in entities)
            {
                solutions.Add(solution.AsDto());
            }
            return solutions;
        }
    }
}
