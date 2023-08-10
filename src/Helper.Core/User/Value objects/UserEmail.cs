using Helper.Core.Inquiry.Exceptions;
using Helper.Core.Inquiry.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.User.Value_objects
{
    public sealed record UserEmail
    {
        public string Value { get; set; }

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
