using Helper.Application.DTO;
using Helper.Core.Inquiry;
using Helper.Core.Offer;

namespace Helper.Infrastructure.DAL.Handlers
{
    public static class Extensions
    {
        public static InquiryDto AsDto(this Inquiry entity)
        => new()
        {
            Description = entity.Description.Value,
            RequestedStartDate = entity.RequestedCompletionDate.Start,
            RequestedEndDate = entity.RequestedCompletionDate.End,
            FeasibilityNote = entity.FeasibilityNote?.Value,
            SolutionDecision = Enum.GetName(entity.SolutionDecision.Value),
            AcceptanceStatus = Enum.GetName(entity.AcceptanceStatus.Value),
            Author = entity.Author,
            RowVersion = entity.RowVersion

        };

        public static OfferDto AsDto(this Offer entity)
       => new()
       {
            InquiryId = entity.InquiryId,
            Description = entity.Description.Value,
            Price = entity.Price.Value,
            IsDraft = entity.IsDraft,
            IsVerified = entity.IsVerified,
            Status = Enum.GetName(entity.Status.Value),
            ClientsReason = entity.ClientsReason.Value,
            RowVersion = entity.RowVersion,
            CreatedAt = entity.CreatedAt,
            ModifiedAt = entity.ModifiedAt,
            IsDeleted = entity.IsDeleted,
            DeletedAt = entity.DeletedAt
       };
    }
}
