using Helper.Core.Inquiry.Exceptions;

namespace Helper.Core.Inquiry
{
    public sealed record InquiryId
    {
        public Guid Value { get; }

        public InquiryId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(InquiryId data) => data.Value;

        public static implicit operator InquiryId(Guid value) => new(value);
    }
}
