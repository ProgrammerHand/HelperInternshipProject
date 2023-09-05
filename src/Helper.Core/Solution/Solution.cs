using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.Solution.ValueObjects;
using Helper.Core.User.Value_objects;
using Helper.Core.Utility;
using Helper.Infrastructure.DAL;

namespace Helper.Core.Solution
{
    public class Solution : IRowVersionControl, ISoftDelete
    {
        public SolutionId Id { get; private set;}
        public InquiryId InquiryId { get; private set;}
        public Description Description { get; private set;}
        public RealisationDate RequestedCompletionDate { get; private set;}
        public SolutionVariant Variant { get; private set;}
        public UserId? AssignedConsultant { get; private set; }
        public byte[] RowVersion { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        private Solution(SolutionId id, Inquiry.Inquiry inquiry)
        {
            Id = new SolutionId(id);
            InquiryId = inquiry.Id;
            Description = inquiry.Description;
            RequestedCompletionDate = inquiry.RequestedCompletionDate;
            Variant = inquiry.SolutionDecision;
        }

        private Solution() { }

        public static Solution CreateSolution(Inquiry.Inquiry inquiry) 
        {
            var id = Guid.NewGuid();
            return new Solution(id, inquiry);
        }

        public void AssignConsultant(Guid userId)
        {
            AssignedConsultant = userId;
        }
    }
}
