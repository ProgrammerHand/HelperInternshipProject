using Helper.Core.ValueObjects;

namespace Helper.Core.Inquiry
{
    internal class Inquiry
    {
        public InquiryId Id { get; private set; }
        public Description Description { get; private set; }
        public CompletionDate RequestedCompletionDate { get; private set; }
        public AcceptanceStatus AcceptanceStatus { get; private set; } = new AcceptanceStatus(Status.awaits_decision);
        public SolutionVariants SolutionDecision { get; private set; }


        private Inquiry(Description ClientDescription, CompletionDate CompletionDate, SolutionVariants Solution)
        {
            Description = ClientDescription;
            RequestedCompletionDate = CompletionDate;
            SolutionDecision = Solution;
        }

        public Inquiry CreateInquiry(Description ClientDescription, CompletionDate CompletionDate, SolutionVariants Solution) 
        {
            return new Inquiry(ClientDescription, CompletionDate, Solution);
        }
    }
}
