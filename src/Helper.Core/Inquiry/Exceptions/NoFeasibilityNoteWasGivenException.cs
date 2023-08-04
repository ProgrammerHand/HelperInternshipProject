using Helper.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.Inquiry.Exceptions
{
    public class NoFeasibilityNoteWasGivenException : CustomException
    {
        public NoFeasibilityNoteWasGivenException() : base("Empty feasibility note given")
        {         
        }
    }
}
