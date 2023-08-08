using Helper.Core.Exceptions;

namespace Helper.Core.Inquiry.Exceptions
{
    public sealed class NoDescriptionGivenException : CustomException
    {
        public NoDescriptionGivenException() : base("No description for inquiry given")
        {
        }
    }
}
