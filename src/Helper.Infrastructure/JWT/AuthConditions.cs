using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Infrastructure.JWT
{
    public sealed class AuthConditions
    {
        public string Issuer { get; set; }
        public string SigningKey { get; set; }
        public TimeSpan? Expiry { get; set; }
    }
}
