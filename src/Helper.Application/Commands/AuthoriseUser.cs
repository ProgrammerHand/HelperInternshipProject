using Helper.Application.Abstractions;

namespace Helper.Application.Commands
{
    public sealed record AuthoriseUser(string Email, string Password) : ICommand;
}
