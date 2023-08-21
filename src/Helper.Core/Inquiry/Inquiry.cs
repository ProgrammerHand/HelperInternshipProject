﻿using Helper.Core.Inquiry.Exceptions;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.User.Value_objects;

namespace Helper.Core.Inquiry
{
    public class Inquiry
    {
        public InquiryId Id { get; private set; }
        public Description Description { get; private set; }
        public RealisationDate RequestedCompletionDate { get; private set; }
        public FeasibilityNote? FeasibilityNote { get; private set; }
        public AcceptanceStatus AcceptanceStatus { get; private set; }
        public SolutionVariant SolutionDecision { get; private set; }
        //public UserId AuthorId { get; private set; }
        public User.User Author { get; private set; }
        public byte[] RowVersion { get; private set; }

        private Inquiry(InquiryId id, Description description, RealisationDate requestedCompletionDate, SolutionVariant solutionDecision, User.User author)
        {
            Id = id;
            Description = description;
            RequestedCompletionDate = requestedCompletionDate;
            SolutionDecision = solutionDecision;
            AcceptanceStatus = new AcceptanceStatus(Status.awaits_decision);
            Author = author;
        }
        private Inquiry()
        {
        }

        public static Inquiry CreateInquiry(Description clientDescription, RealisationDate completionDate, SolutionVariant solution, User.User author)
        {
            InquiryId id = Guid.NewGuid();
            return new Inquiry(id, clientDescription, completionDate, solution, author);
        }

        public void AcceptInquiry()
        {
            if (string.IsNullOrEmpty(FeasibilityNote?.Value) || string.IsNullOrWhiteSpace(FeasibilityNote.Value))
                throw new NoFeasibilityNoteWasGivenException();
            AcceptanceStatus = new AcceptanceStatus(Status.accepted);
        }

        public void RejectInquiry()
        {
            if (string.IsNullOrEmpty(FeasibilityNote?.Value) || string.IsNullOrWhiteSpace(FeasibilityNote.Value))
                throw new NoFeasibilityNoteWasGivenException();
            AcceptanceStatus = new AcceptanceStatus(Status.rejected);
        }

        public void SetFeasibilityNote(FeasibilityNote feasibilityNote)
        {
            if (string.IsNullOrEmpty(feasibilityNote.Value) || string.IsNullOrWhiteSpace(feasibilityNote.Value))
                throw new NoFeasibilityNoteWasGivenException();
            FeasibilityNote = feasibilityNote;
        }

        public void ChangeAuthor(User.User author)
        {
            Author = author;
        }
    }
}
