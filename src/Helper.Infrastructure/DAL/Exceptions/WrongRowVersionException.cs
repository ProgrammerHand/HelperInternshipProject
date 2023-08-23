using Helper.Core.Exceptions;

namespace Helper.Infrastructure.DAL.Exceptions
{
    public sealed class WrongRowVersionException : CustomException
    {
        public WrongRowVersionException() : base("Given RowVersion expired")
        {
        }
    }
}
