using Helper.Core.Inquiry.Exceptions;

namespace Helper.Core.Offer
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
