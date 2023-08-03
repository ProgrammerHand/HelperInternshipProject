namespace Helper.Core.Entities
{
    internal class Inquiri
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public DateOnly RequestedCompletionDate { get; private set; }
        public string SolutionDecision { get; private set; } = "Awaits decision";

        public Inquiri(string description, DateOnly requestedCompletionDate)
        {
            Description = description;
            RequestedCompletionDate = requestedCompletionDate;
        }
    }
}
