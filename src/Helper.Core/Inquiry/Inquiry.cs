using Helper.Core.ValueObjects;

namespace Helper.Core.Entities
{
    internal class Inquiry
    {
        public string Description { get; private set; }
        public DateOnly RequestedCompletionDate { get; private set; }
        public AcceptanceStatus AcceptanceStatus { get; private set; } = new AcceptanceStatus(Status.awaits_decision);
        public SolutionVariants SolutionDecision { get; private set; }


        public Inquiry(string description, DateOnly requestedCompletionDate)
        {
            Description = description;
            RequestedCompletionDate = requestedCompletionDate;
        }
    }
}
