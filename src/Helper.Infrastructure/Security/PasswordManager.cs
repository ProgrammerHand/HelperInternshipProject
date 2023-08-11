using Helper.Application.Security;
using Helper.Core.User;
using Microsoft.AspNetCore.Identity;

namespace Helper.Infrastructure.Security
{
    internal sealed class PasswordManager : IPasswordManager
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordManager(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        public string Hash(string password) => _passwordHasher.HashPassword(default, password);

        public bool Validate(string password, string hashedPassword) =>
            _passwordHasher.VerifyHashedPassword(default, hashedPassword, password) == PasswordVerificationResult.Success;
    }
}
