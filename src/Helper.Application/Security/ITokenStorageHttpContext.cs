using Helper.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Application.Security
{
    public interface ITokenStorageHttpContext
    {
        void SetToken(JwtDto token);
        JwtDto GetToken();
    }
}
