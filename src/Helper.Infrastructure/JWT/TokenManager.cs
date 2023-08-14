using Helper.Application.DTO;
using Helper.Application.Security;
using Helper.Core;
using Helper.Core.User.Value_objects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Infrastructure.JWT
{
    internal sealed class TokenManager : ITokenManager
    {
        private readonly IClockCustom _clock;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly TimeSpan _expiry;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtSecurityTokenHandler _jwtSecurityToken = new JwtSecurityTokenHandler();

        public TokenManager(IOptions<AuthConditions> options, IClockCustom clock)
        {
            _clock = clock;
            _issuer = options.Value.Issuer;
            _audience = options.Value.Audience;
            _expiry = options.Value.Expiry ?? TimeSpan.FromHours(1);
            _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(options.Value.SigningKey)),
                    SecurityAlgorithms.HmacSha512);
        }

        public JwtDto CreateToken(Guid userId, Roles role)
        {
            var now = _clock.Now;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            var expiry = now.Add(_expiry);

            JwtSecurityToken tokenPrefab = new JwtSecurityToken(issuer: _issuer, audience: _audience, claims: claims, notBefore: now, expires: expiry, signingCredentials: _signingCredentials);

            return new JwtDto
            {
                AccessToken = _jwtSecurityToken.WriteToken(tokenPrefab)
            };
        }
    }
}
