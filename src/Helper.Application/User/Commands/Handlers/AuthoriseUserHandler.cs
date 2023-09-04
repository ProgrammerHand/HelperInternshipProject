using Helper.Application.Abstraction.Commands;
using Helper.Application.Exceptions;
using Helper.Application.Security;
using Helper.Core.User;
using Microsoft.AspNetCore.Http;

namespace Helper.Application.User.Commands.Handlers
{
    public sealed class AuthoriseUserHandler : ICommandHandler<AuthoriseUser>
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordManager _passwordManager;
        private readonly ITokenManager _tokenManager;
        private readonly ITokenStorageHttpContext _tokenStorage;

        public AuthoriseUserHandler(IUserRepository userRepo, IPasswordManager passwordManager,
            ITokenStorageHttpContext tokenStorage, ITokenManager tokenManager)
        {
            _userRepo = userRepo;
            _passwordManager = passwordManager;
            _tokenManager = tokenManager;
            _tokenStorage = tokenStorage;
        }

        public async Task HandleAsync(AuthoriseUser command)
        {
            if (await _userRepo.CheckByEmailAsync(command.Email) is false)
                throw new UserDoesntExistException();
            var entity = await _userRepo.GetByEmailAsync(command.Email);
            if (_passwordManager.Validate(command.Password, entity.PasswordHash) is false)
                throw new ArgumentException("wrong pasword");
            var token = _tokenManager.CreateToken(entity.Id, entity.Role);
            _tokenStorage.SetToken(token);
        }
    }
}
