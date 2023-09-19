using Helper.Application.DTO;
using Helper.Core.Inquiry;
using Helper.Core.Offer;
using Helper.Core.User;

namespace Helper.Infrastructure.DAL.Handlers
{
    public static class Extensions
    {
        public static InquiryDto AsDto(this Inquiry entity)
        => new()
        {
            Id = entity.Id,
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
            Id = entity.Id,
            InquiryId = entity.InquiryId,
            Description = entity.Description.Value,
            Price = entity.Price is null ? 0 : entity.Price.Value,
            IsDraft = entity.IsDraft,
            PaymentDate = entity.PaymentDate,
            Status = Enum.GetName(entity.Status.Value),
            ClientsReason = entity.ClientsReason is null ? null : entity.ClientsReason.Value,
            RowVersion = entity.RowVersion,
            CreatedAt = entity.CreatedAt,
            ModifiedAt = entity.ModifiedAt,
            IsDeleted = entity.IsDeleted,
            DeletedAt = entity.DeletedAt
       };

        public static UserDto AsDto(this User entity)
       => new()
       {
           Id = entity.Id,
           Email = entity.Email,
           Role = Enum.GetName(entity.Role.Value),
           CreatedAt = entity.CreatedAt
       };
    }
}
