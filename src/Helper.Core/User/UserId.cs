﻿using Helper.Core.Exceptions;

namespace Helper.Core.User
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
