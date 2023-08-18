using Helper.Application.Abstraction.Commands;

namespace Helper.Application.User.Commands
{
    public sealed record RegisterUser(string Email, string Password) : ICommand;
}
