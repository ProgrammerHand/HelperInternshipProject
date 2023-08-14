using Helper.Application.DTO;
using Helper.Application.Security;
using Microsoft.AspNetCore.Http;

namespace Helper.Infrastructure.JWT
{
    internal sealed class TokenStorageHttpContext : ITokenStorageHttpContext
    {
        private const string TokenType = "bearer";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenStorageHttpContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetToken(JwtDto token) => _httpContextAccessor.HttpContext.Items.TryAdd(TokenType, token);

        public JwtDto GetToken()
        {
            if (_httpContextAccessor.HttpContext is null)
                return null;

            if (_httpContextAccessor.HttpContext.Items.TryGetValue(TokenType, out var token))
                return token as JwtDto;

            return null;
        }
    }
}
