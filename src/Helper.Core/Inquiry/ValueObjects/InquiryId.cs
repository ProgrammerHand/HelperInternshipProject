using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.Inquiry.ValueObjects
{
    // remake all valueobj to records
    public sealed record InquiryId
    {
        Guid Value { get; set; }

        public InquiryId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            Value = value;
        }

        public static implicit operator Guid(InquiryId date) => date.Value;

        public static implicit operator InquiryId(Guid value) => new(value);
    }
}
