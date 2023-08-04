using Helper.Core.Exceptions;

namespace Helper.Application.Exceptions
{
    public sealed class StartDateInPastException : CustomException
    {
        public StartDateInPastException() : base("Inquiry request expired")
        {
        }
    }
}
