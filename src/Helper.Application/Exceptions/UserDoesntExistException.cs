using Helper.Core.Exceptions;

namespace Helper.Application.Exceptions
{
    public sealed class UserDoesntExistException : CustomException
    {
        public UserDoesntExistException() : base("User doesnt exist")
        {
            
        }
    }
}
