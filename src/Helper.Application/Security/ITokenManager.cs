using Helper.Application.DTO;
using Helper.Core.User.Value_objects;

namespace Helper.Application.Security
{
    public interface ITokenManager
    {
        JwtDto CreateToken(Guid userId, Roles role);
    }
}
