using Helper.Core.Exceptions;

namespace Helper.Application.Exceptions
{
    public sealed class EndBeforeStartException : CustomException
    {
        public EndBeforeStartException() : base("Requested end date before start date")
        {
        }
    }
}
