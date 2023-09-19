using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;
using Helper.Application.ReservedEmployeeTime.Queries;
using Microsoft.EntityFrameworkCore;

namespace Helper.Infrastructure.DAL.Handlers
{
    internal class GetAvailableEmployeeHandler : IQueryHandler<GetAvailableEmployee, List<UserDto>>
    {
        private readonly HelperDbContext _context;
        public GetAvailableEmployeeHandler(HelperDbContext DbContext) => _context = DbContext;
        public async Task<List<UserDto>> HandleAsync(GetAvailableEmployee querry)
        {
            var busyEmployee = await _context.ReservedEmployeeTime
                .Where(x => ((x.Start < querry.RealisationStart && querry.RealisationStart < x.End) 
                && (x.Start < querry.RealisationEnd && querry.RealisationEnd < x.End)))
                .Select(x => x.WorkerId).AsNoTracking().ToListAsync();
            var entities = await _context.Users.Where(x => !busyEmployee.Contains(x.Id)).AsNoTracking().ToListAsync();
            var freeEmployee = new List<UserDto>();
            foreach (var entity in entities)
            {
                freeEmployee.Add(entity.AsDto());
            }
            return freeEmployee;
        }
    }
}
