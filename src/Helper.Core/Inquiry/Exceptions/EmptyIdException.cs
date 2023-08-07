using Helper.Core.Exceptions;

namespace Helper.Core.Inquiry.Exceptions
{
    public class EmptyIdException : CustomException
    {
        public EmptyIdException() : base("Empty Id was given")
        {
        }
    }
}
