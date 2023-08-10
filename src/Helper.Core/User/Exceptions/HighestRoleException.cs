using Helper.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.User.Exceptions
{
    public sealed class HighestRoleException : CustomException
    {
        public HighestRoleException() : base("Alredy have highest role")
        {
            
        }
    }
}
