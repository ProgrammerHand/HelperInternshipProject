using Helper.Application.DTO;
using Helper.Core.Inquiry;

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

        };
    }
}
