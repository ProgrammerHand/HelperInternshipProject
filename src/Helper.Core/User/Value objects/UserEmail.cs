using Helper.Core.Exceptions;

namespace Helper.Core.User.Value_objects
{
    public sealed record UserEmail
    {
        public string Value { get; }

        public UserEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace((email)))
            {
                throw new EmptyIdException();
            }
            Value = email;
        }

        private UserEmail()
        {
        }

        public static implicit operator string(UserEmail data) => data.Value;

        public static implicit operator UserEmail(string mail) => new(mail);
    }
}
