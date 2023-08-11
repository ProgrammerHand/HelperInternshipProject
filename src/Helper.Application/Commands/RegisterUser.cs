using Helper.Application.Abstractions;

namespace Helper.Application.Commands
{
    public sealed record RegisterUser(string Email, string Password) : ICommand;
}
