using Helper.Application.Abstraction.Commands;
using Helper.Application.ReservedEmployeeTime;
using Helper.Core.Solution;

namespace Helper.Application.Solution.Commands.Handlers
{
    public class AssignConsultantHandler : ICommandHandler<AssignConsultant>
    {
        private readonly ISolutionRepository _solutionRepo;
        private readonly IEmployeeReservation _employeeReservation;

        public AssignConsultantHandler(ISolutionRepository solutionRepo, IEmployeeReservation employeeReservation)
        {
            _solutionRepo = solutionRepo;
            _employeeReservation = employeeReservation;
        }
        public async Task HandleAsync(AssignConsultant command)
        {
            var solution = await _solutionRepo.GetByIdAsync(command.SolutionId);
            var reservationId = await _employeeReservation.ReserveEmployee(command.UserId, solution.RequestedCompletionDate.Start, solution.RequestedCompletionDate.End);
            solution.AssignConsultant(command.UserId);
            solution.AddTimeReservation(reservationId);
            await _solutionRepo.UpdateAsync(solution);
        }
    }
}
