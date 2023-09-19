using Helper.Application.Abstraction.Events;

namespace Helper.Application.Solution.Events
{
    public sealed record ConsultantAssigned(Core.Solution.Solution Solution) : IEvent;
}
