using Helper.Application.Security;
using Helper.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Helper.Infrastructure.JWT
{
    internal static class Extensions
    {
        private const string OptionsSectionName = "token";

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var conditions = configuration.GetOptions<AuthConditions>(OptionsSectionName);

            services
            .Configure<AuthConditions>(configuration.GetRequiredSection(OptionsSectionName))
            .AddSingleton<ITokenManager, TokenManager>()
            .AddSingleton<ITokenStorageHttpContext, TokenStorageHttpContext>()
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Audience = conditions.Audience;
                x.IncludeErrorDetails = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = conditions.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conditions.SigningKey))
                };
            });

            services.AddAuthorization(authorization =>
            {
                authorization.AddPolicy("is-admin", policy =>
                {
                    policy.RequireRole("admin");
                });
                authorization.AddPolicy("is-worker", policy =>
                {
                    policy.RequireRole("Admin");
                    policy.RequireRole("Consultant");
                });
            });

            return services;
        }
    }
}
