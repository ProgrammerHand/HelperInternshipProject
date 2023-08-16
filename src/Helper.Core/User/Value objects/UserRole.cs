using Helper.Core.Inquiry.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.User.Value_objects
{
    public sealed record UserRole
    {
        public Role Value { get; set; }

        public UserRole(Role role)
        {
            Value = role;
        }

        private UserRole()
        {
            
        }

        public static implicit operator Role(UserRole data) => data.Value;

        public static implicit operator UserRole(Role role) => new(role);
    }
}
