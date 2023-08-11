using Helper.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Application.Exceptions
{
    public sealed class UserAlredyExistException : CustomException
    {
        public UserAlredyExistException() : base("User with such email alredy exists")
        {
            
        }
    }
}
