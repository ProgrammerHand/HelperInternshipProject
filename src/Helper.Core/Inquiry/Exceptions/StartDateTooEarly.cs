using Helper.Core.Exceptions;

namespace Helper.Core.Inquiry.Exceptions
{
    public sealed class StartDateTooEarly : CustomException
    {
        public StartDateTooEarly() : base("Given start date too early, 7 days needed")
        {
        }
    }
}
