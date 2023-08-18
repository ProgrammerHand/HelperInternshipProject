using Helper.Core.Inquiry.Exceptions;
using Helper.Core.Inquiry.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.Offer.ValueObjects
{
    public sealed record OfferId
    {
        public Guid Value { get; }

        public OfferId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(OfferId data) => data.Value;

        public static implicit operator OfferId(Guid value) => new(value);
    }
}
