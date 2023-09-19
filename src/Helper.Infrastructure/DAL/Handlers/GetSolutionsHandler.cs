using Helper.Application.Abstraction.Queries;
using Helper.Application.Solution.Queries;
using Helper.Core.Solution;
using Microsoft.EntityFrameworkCore;

namespace Helper.Infrastructure.DAL.Handlers
{
    internal class GetSolutionsHandler : IQueryHandler<GetSolutions, List<Solution>>
    {
        private readonly HelperDbContext _context;
        public GetSolutionsHandler(HelperDbContext DbContext) => _context = DbContext;
        public async Task<List<Solution>> HandleAsync(GetSolutions querry)
        {
            var entities = await _context.Solutions.ToListAsync();
            //var offers = new List<OfferDto>();
            //foreach (var entity in entities)
            //{
            //    offers.Add(entity.AsDto());
            //}
            return entities;
        }
    }
}
