using Helper.Application.Abstractions;
using Helper.Core.ValueObjects;

namespace Helper.Application.Commands
{
    public sealed record AcceptInquiry(int InquiriId, Status value) : ICommand;
}
