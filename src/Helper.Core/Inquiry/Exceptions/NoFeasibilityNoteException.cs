using Helper.Core.Exceptions;

namespace Helper.Core.Inquiry.Exceptions
{
    public class NoFeasibilityNoteException : CustomException
    {
        public NoFeasibilityNoteException() : base("Empty feasibility note given")
        {         
        }
    }
}
