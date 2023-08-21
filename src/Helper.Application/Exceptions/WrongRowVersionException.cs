using Helper.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Application.Exceptions
{
    public sealed class WrongRowVersionException : CustomException
    {
        public WrongRowVersionException() : base("Given RowVersion expired")
        {
        }
    }
}
