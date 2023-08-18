using Helper.Application.Abstraction.Commands;
using Helper.Core.Inquiry.ValueObjects;

namespace Helper.Application.Inquiry.Commands
{
    public sealed record CreateInquiry(string Description, DateTime Start, DateTime? End, Variants SolutionVariant, Guid AuthorId) : ICommand;
}
