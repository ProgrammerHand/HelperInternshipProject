using Helper.Core.Inquiry.Exceptions;
using Helper.Core.Offer.Exceptions;

namespace Helper.Core.Offer.ValueObjects
{
    public sealed record OfferPrice
    {
        public double Value { get; }

        public OfferPrice(double value)
        {
            if (value <= 0)
            {
                throw new InccorectPrice();
            }
            Value = value;
        }

        public static implicit operator double(OfferPrice data) => data.Value;

        public static implicit operator OfferPrice(double body) => new(body);
    }
}
