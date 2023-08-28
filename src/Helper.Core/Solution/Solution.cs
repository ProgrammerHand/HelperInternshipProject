﻿using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.Solution.ValueObjects;

namespace Helper.Core.Solution
{
    public class Solution
    {
        public SolutionId Id { get; private set;}
        public InquiryId InquiryId { get; private set;}
        public Description Description { get; private set;}
        public RealisationDate RequestedCompletionDate { get; private set;}
        public SolutionVariant Variant { get; private set;}


        private Solution(SolutionId id, Inquiry.Inquiry inquiry)
        {
            Id = new SolutionId(id);
            InquiryId = inquiry.Id;
            Description = inquiry.Description;
            RequestedCompletionDate = inquiry.RequestedCompletionDate;
            Variant = inquiry.SolutionDecision;
        }

        public Solution CreateSolution(Inquiry.Inquiry inquiry) 
        {
            var id = Guid.NewGuid();
            return new Solution(id, inquiry);
        }
    }
}