using Helper.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Application.Exceptions
{
    public sealed class StartDateTooEarly : CustomException
    {
        public StartDateTooEarly() : base("Given start date too early, 5 days needed")
        {
        }
    }
}
