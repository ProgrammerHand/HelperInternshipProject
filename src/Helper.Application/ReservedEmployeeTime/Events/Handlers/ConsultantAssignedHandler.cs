using Helper.Application.Abstraction.Events;
using Helper.Application.Solution.Events;
using Helper.Core.ReservedEmployeeTime;
using Helper.Core.Solution;

namespace Helper.Application.ReservedEmployeeTime.Events.Handlers
{
    public class ConsultantAssignedHandler : IEventHandler<ConsultantAssigned>
    {
        private readonly IReservedEmployeeTimeRepository _reservedEmployeeRepo;
        private readonly ISolutionRepository _solutionRepo;

        public ConsultantAssignedHandler(IReservedEmployeeTimeRepository reservedEmployeeRepo, ISolutionRepository solutionRepo)
        {
            _reservedEmployeeRepo = reservedEmployeeRepo;
            _solutionRepo = solutionRepo;
        }

        public async Task HandleAsync(ConsultantAssigned @event)
        {
            var reservation = Core.ReservedEmployeeTime.ReservedEmployeeTime.CreateEmployeeReservation(@event.Solution.AssignedConsultant, @event.Solution.RequestedCompletionDate.Start, @event.Solution.RequestedCompletionDate.End);
            @event.Solution.AddTimeReservation(reservation.Id);
            await _reservedEmployeeRepo.AddAsync(reservation);
            await _solutionRepo.UpdateAsync(@event.Solution);
        }
    }
}
