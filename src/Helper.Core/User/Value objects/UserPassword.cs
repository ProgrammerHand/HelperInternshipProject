using Helper.Core.Inquiry.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.User.Value_objects
{
    public sealed record UserPassword
    {
        public string Value { get; set; }
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
