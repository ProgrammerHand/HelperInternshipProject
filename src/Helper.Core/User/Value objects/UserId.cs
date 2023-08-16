using Helper.Core.Inquiry.Exceptions;
using Helper.Core.Inquiry.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.User.Value_objects
{
    public sealed record UserId
    {
        public Guid Value { get; }

        public UserId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(UserId date) => date.Value;

        public static implicit operator UserId(Guid value) => new(value);
    }
}
