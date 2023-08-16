using Helper.Core.Inquiry.Exceptions;

namespace Helper.Core.User.Value_objects
{
    public sealed record UserPassword
    {
        public string Value { get; }
        public UserPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace((password)))
            {
                throw new EmptyIdException();
            }
            Value = password;
        }

        private UserPassword()
        {
            
        }

        public static implicit operator string(UserPassword data) => data.Value;

        public static implicit operator UserPassword(string password) => new(password);
    }
}
