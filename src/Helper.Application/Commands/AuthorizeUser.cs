using Helper.Application.Abstractions;

namespace Helper.Application.Commands
{
    public sealed record AuthorizeUser(string Email, string Password) : ICommand;
}
