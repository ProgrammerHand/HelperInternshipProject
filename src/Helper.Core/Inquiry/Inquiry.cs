using Helper.Core.Inquiry.ValueObjects;

namespace Helper.Core.Inquiry
{
    public class Inquiry
    {
        public InquiryId Id { get; private set; }
        public Description Description { get; private set; }
        public RealisationDate RequestedCompletionDate { get; private set; }
        public AcceptanceStatus AcceptanceStatus { get; private set; } = new AcceptanceStatus(Status.awaits_decision);
        public SolutionVariants SolutionDecision { get; private set; }


        private Inquiry( InquiryId id, Description clientDescription, RealisationDate completionDate, SolutionVariants solution)
        {
            Id = id;
            Description = clientDescription;
            RequestedCompletionDate = completionDate;
            SolutionDecision = solution;
        }

        public static Inquiry CreateInquiry(Description clientDescription, RealisationDate completionDate, SolutionVariants solution) 
        {
            InquiryId id = Guid.NewGuid();
            return new Inquiry(id, clientDescription, completionDate, solution);
        }
    }
}
