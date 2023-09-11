using Helper.Application.Abstraction.Commands;
using Helper.Core.Inquiry.ValueObjects;
using System.Text.Json.Serialization;

namespace Helper.Application.Inquiry.Commands
{
    public sealed record CreateInquiry(string Description, DateTime Start, DateTime? End, Variants SolutionVariant, [property: JsonIgnore] Guid AuthorId) : ICommand;
}
