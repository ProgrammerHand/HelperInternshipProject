using Helper.Application.Abstraction.Commands;
using Helper.Application.Abstraction.Events;
using Helper.Application.Solution.Events;
using Helper.Core.Solution;

namespace Helper.Application.Solution.Commands.Handlers
{
    public class AssignConsultantHandler : ICommandHandler<AssignConsultant>
    {
        private readonly ISolutionRepository _solutionRepo;
        private readonly IEventDispatcher _eventDispatcher;

        public AssignConsultantHandler(ISolutionRepository solutionRepo, IEventDispatcher eventDispatcher)
        {
            _solutionRepo = solutionRepo;
            _eventDispatcher = eventDispatcher;
        }
        public async Task HandleAsync(AssignConsultant command)
        {
            var solution = await _solutionRepo.GetByIdAsync(command.SolutionId);
            solution.AssignConsultant(command.UserId);
            await _solutionRepo.UpdateAsync(solution);
            await _eventDispatcher.PublishAsync(new ConsultantAssigned(solution));
        }
    }
}
