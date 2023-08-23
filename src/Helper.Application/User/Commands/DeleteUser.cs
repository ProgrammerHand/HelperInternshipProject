using Helper.Application.Abstraction.Commands;

namespace Helper.Application.User.Commands
{
    public sealed record DeleteUser(Guid UserId) : ICommand;
}
