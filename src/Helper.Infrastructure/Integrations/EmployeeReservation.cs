using Helper.Application.DTO;
using Helper.Core.ReservedEmployeeTime;
using Helper.Core.Solution;
using Helper.Core.User.Value_objects;
using Helper.Infrastructure.DAL;
using Helper.Infrastructure.DAL.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Helper.Application.ReservedEmployeeTime
{
    public class EmployeeReservation : IEmployeeReservation
    {

        private readonly HelperDbContext _context;
        private readonly IReservedEmployeeTimeRepository _reservedEmployeeRepo;
        private readonly ISolutionRepository _solutionRepository;

        public EmployeeReservation(HelperDbContext DbContext, IReservedEmployeeTimeRepository reservedEmployeeRepo, ISolutionRepository solutionRepository)
        {
            _context = DbContext;
            _reservedEmployeeRepo = reservedEmployeeRepo;
            _solutionRepository = solutionRepository;
        }
        public async Task<List<UserDto>> GetAvailableEmployee(Guid solutionId)
        {
            var solution = await _solutionRepository.GetByIdAsync(solutionId);
            var busyEmployee = await _context.ReservedEmployeeTime
                .Where(x => ((x.Start < solution.RequestedCompletionDate.Start && solution.RequestedCompletionDate.Start < x.End)
                && (x.Start < solution.RequestedCompletionDate.End && solution.RequestedCompletionDate.End < x.End)))
                .Select(x => x.WorkerId).AsNoTracking().ToListAsync();
            var entities = await _context.Users.Where(x => !busyEmployee.Contains(x.Id)).AsNoTracking().ToListAsync();
            var freeEmployee = new List<UserDto>();
            foreach (var entity in entities)
            {
                if(entity.Role != Role.User)
                    freeEmployee.Add(entity.AsDto());
            }
            return freeEmployee;
        }

        public async Task<ReservedEmployeeTimeId> ReserveEmployee(Guid userId, DateTime RealisationStart, DateTime? RealisationEnd)
        {
            var reservation = Core.ReservedEmployeeTime.ReservedEmployeeTime.CreateEmployeeReservation(userId, RealisationStart, RealisationEnd);
            await _reservedEmployeeRepo.AddAsync(reservation);
            return reservation.Id;
        }
    }
}
