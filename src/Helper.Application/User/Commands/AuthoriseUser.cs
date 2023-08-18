using Helper.Application.Abstraction.Commands;

namespace Helper.Application.User.Commands
{
    public sealed record AuthoriseUser(string Email, string Password) : ICommand;
}
