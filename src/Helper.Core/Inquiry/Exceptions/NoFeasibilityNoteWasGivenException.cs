using Helper.Core.Exceptions;

namespace Helper.Core.Inquiry.Exceptions
{
    public class NoFeasibilityNoteWasGivenException : CustomException
    {
        public NoFeasibilityNoteWasGivenException() : base("Empty feasibility note given")
        {         
        }
    }
}
