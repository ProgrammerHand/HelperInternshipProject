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
        public Roles Value { get; set; }

        public UserRole(Roles role)
        {
            Value = role;
        }

        private UserRole()
        {
            
        }

        public static implicit operator Roles(UserRole data) => data.Value;

        public static implicit operator UserRole(Roles role) => new(role);
    }
}
