using Helper.Core.Exceptions;
using Helper.Core.Inquiry.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.Offer.ValueObjects
{
    public sealed record OfferDescription
    {
        public string Value { get; }

        public OfferDescription(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                throw new NoDescriptionGivenException();
            }

            Value = value;
        }

        public static implicit operator string(OfferDescription data) => data.Value;

        public static implicit operator OfferDescription(string value) => new(value);
    }
}
