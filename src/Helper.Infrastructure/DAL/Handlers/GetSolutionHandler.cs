using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;
using Helper.Application.Solution.Queries;
using Helper.Core.Solution;
using Microsoft.EntityFrameworkCore;

namespace Helper.Infrastructure.DAL.Handlers
{
    internal class GetSolutionHandler : IQueryHandler<GetSolution, SolutionDto>
    {
        private readonly HelperDbContext _context;

        public GetSolutionHandler(HelperDbContext DbContext) => _context = DbContext;
        public async Task<SolutionDto> HandleAsync(GetSolution query)
        {
            var solutionId = new SolutionId(query.Id);
            var solution = await _context.Solutions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == solutionId);
            return solution?.AsDto();
        }
    }
}
