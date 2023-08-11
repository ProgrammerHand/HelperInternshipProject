using Helper.Application.Abstractions;
using Helper.Core.User;

namespace Helper.Application.Commands.Handlers
{
    public sealed class AuthorizeUserHandler : ICommandHandler<AuthorizeUser>
    {
        private readonly IUserRepository _userRepo;

        public AuthorizeUserHandler(IUserRepository inquiryRepo)
        {
            _userRepo = inquiryRepo;
        }

        public async Task HandleAsync(AuthorizeUser command)
        {
        
        }
    }
}
