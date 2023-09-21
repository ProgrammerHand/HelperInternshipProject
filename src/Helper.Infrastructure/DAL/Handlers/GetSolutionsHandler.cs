using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;
using Helper.Application.Solution.Queries;
using Helper.Core.Solution;
using Microsoft.EntityFrameworkCore;

namespace Helper.Infrastructure.DAL.Handlers
{
    internal class GetSolutionsHandler : IQueryHandler<GetSolutions, List<SolutionDto>>
    {
        private readonly HelperDbContext _context;
        public GetSolutionsHandler(HelperDbContext DbContext) => _context = DbContext;
        public async Task<List<SolutionDto>> HandleAsync(GetSolutions querry)
        {
            var entities = await _context.Solutions.ToListAsync();
            var solutions = new List<SolutionDto>();
            foreach (var solution in entities)
            {
                solutions.Add(solution.AsDto());
            }
            return solutions;
        }
    }
}
