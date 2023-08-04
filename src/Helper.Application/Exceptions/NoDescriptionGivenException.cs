using Helper.Core.Exceptions;

namespace Helper.Application.Exceptions
{
    public sealed class NoDescriptionGivenException : CustomException
    {
        public NoDescriptionGivenException() : base("No description was given")
        {
        }
    }
}
