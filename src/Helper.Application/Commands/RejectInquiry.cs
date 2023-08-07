using Helper.Application.Abstractions;
using Helper.Core.Inquiry.ValueObjects;

namespace Helper.Application.Commands
{
    public sealed record RejectInquiry(Guid InquiriId) : ICommand;
}
