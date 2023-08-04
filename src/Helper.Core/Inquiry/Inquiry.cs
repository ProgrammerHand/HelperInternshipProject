using Helper.Core.Inquiry.Exceptions;
using Helper.Core.Inquiry.ValueObjects;

namespace Helper.Core.Inquiry
{
    public class Inquiry
    {
        public InquiryId Id { get; private set; }
        public Description Description { get; private set; }
        public RealisationDate RequestedCompletionDate { get; private set; }
        public FeasibilityNote FeasibilityNote { get; private set; }
        public AcceptanceStatus AcceptanceStatus { get; private set; }
        public SolutionVariants? SolutionDecision { get; private set; }


        private Inquiry( InquiryId id, Description clientDescription, RealisationDate completionDate, SolutionVariants solution)
        {
            Id = id;
            Description = clientDescription;
            RequestedCompletionDate = completionDate;
            SolutionDecision = solution;
            AcceptanceStatus = new AcceptanceStatus(Status.awaits_decision);
        }

        public static Inquiry CreateInquiry(Description clientDescription, RealisationDate completionDate, SolutionVariants solution) 
        {
            InquiryId id = Guid.NewGuid();
            return new Inquiry(id, clientDescription, completionDate, solution);
        }

        public void AcceptInquiry() 
        {
            if (string.IsNullOrEmpty(FeasibilityNote.Body) || string.IsNullOrWhiteSpace(FeasibilityNote.Body))
                throw new NoFeasibilityNoteWasGivenException();
            AcceptanceStatus = new AcceptanceStatus(Status.accepted);
        }

        public void RejectInquiry()
        {
            if (string.IsNullOrEmpty(FeasibilityNote.Body) || string.IsNullOrWhiteSpace(FeasibilityNote.Body))
                throw new NoFeasibilityNoteWasGivenException();
            AcceptanceStatus = new AcceptanceStatus(Status.rejected);
        }

        public void SetFeasibilityNote(FeasibilityNote feasibilityNote)
        {
            if (string.IsNullOrEmpty(feasibilityNote.Body) || string.IsNullOrWhiteSpace(feasibilityNote.Body))
                throw new NoFeasibilityNoteWasGivenException();
            FeasibilityNote = feasibilityNote;
        }
    }
}
