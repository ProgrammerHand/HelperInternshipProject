using Helper.Core.Exceptions;

namespace Helper.Core.Inquiry.Exceptions
{
    public sealed class StartDateInPastException : CustomException
    {
        public StartDateInPastException() : base("Inquiry request expired")
        {
        }
    }
}
