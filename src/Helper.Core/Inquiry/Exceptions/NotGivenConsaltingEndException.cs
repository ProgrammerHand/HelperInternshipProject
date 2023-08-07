using Helper.Core.Exceptions;

namespace Helper.Core.Inquiry.Exceptions
{
    public class NotGivenConsaltingEndException : CustomException
    {
        public NotGivenConsaltingEndException() : base("No description for inquiry given")
        {
        }

    }
}
