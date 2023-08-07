using Helper.Core.Inquiry.Exceptions;

namespace Helper.Core.Inquiry.ValueObjects
{
    public sealed record InquiryId
    {
       public Guid Value { get; set; }

        public InquiryId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyIdException();
            }
             
            Value = value;
        }

        public static implicit operator Guid(InquiryId date) => date.Value;

        public static implicit operator InquiryId(Guid value) => new(value);
    }
}
