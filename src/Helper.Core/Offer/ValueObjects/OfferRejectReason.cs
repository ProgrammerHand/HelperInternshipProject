using Helper.Core.Exceptions;

namespace Helper.Core.Offer.ValueObjects
{
    public sealed record OfferRejectReason
    {
        public string Value { get; private set; }

        public OfferRejectReason(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace((value)))
                throw new NoDescriptionGivenException();
            Value = value;
        }

        public void Extend(string value) 
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace((value)))
                throw new NoDescriptionGivenException();
            Value += value;
        }

        public static implicit operator string(OfferRejectReason data) => data.Value;

        public static implicit operator OfferRejectReason(string body) => new(body);
    }
}
