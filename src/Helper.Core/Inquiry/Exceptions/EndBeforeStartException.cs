using Helper.Core.Exceptions;

namespace Helper.Core.Inquiry.Exceptions
{
    public sealed class EndBeforeStartException : CustomException
    {
        public EndBeforeStartException() : base("Requested end date before start date")
        {
        }
    }
}
