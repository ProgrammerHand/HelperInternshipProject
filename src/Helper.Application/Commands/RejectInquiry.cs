using Helper.Application.Abstractions;
using Helper.Core.ValueObjects;

namespace Helper.Application.Commands
{
    public sealed record RejectInquiry(int InquiriId, Status Value) : ICommand;
}
