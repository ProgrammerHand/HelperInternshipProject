using Helper.Application.Abstraction.Commands;
using Helper.Application.Exceptions;
using Helper.Core.User;

namespace Helper.Application.User.Commands.Handlers
{
    internal class DeleteUserHandler : ICommandHandler<DeleteUser>
    {
        private readonly IUserRepository _userRepo;

        public DeleteUserHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task HandleAsync(DeleteUser command)
        {
            var inquiry = await _userRepo.GetByIdAsync(command.UserId) ?? throw new UserDoesntExistException();
            await _userRepo.DeleteUser(inquiry);
        }
    }
}
