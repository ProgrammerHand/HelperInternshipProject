using Helper.Application.Abstractions;
using Helper.Application.Security;
using Helper.Core.User;
using Helper.Core.User.Value_objects;

namespace Helper.Application.Commands.Handlers
{
    public sealed class RegisterUserHandler : ICommandHandler<RegisterUser>
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordManager _passwordManager;

        public RegisterUserHandler(IUserRepository inquiryRepo, IPasswordManager passwordManager)
        {
            _userRepo = inquiryRepo;
            _passwordManager = passwordManager;
        }

        public async Task HandleAsync(RegisterUser command)
        {
            var user = User.CreateUser(new UserEmail(command.Email), new UserPassword(_passwordManager.Hash(command.Password)));
            await _userRepo.AddAsync(user);
        }
    }
}