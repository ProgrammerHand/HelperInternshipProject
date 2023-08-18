using Helper.Application.Abstraction.Commands;
using Helper.Core.Inquiry.ValueObjects;

namespace Helper.Application.Inquiry.Commands
{
    public sealed record RejectInquiry(Guid InquiriId) : ICommand;
}
