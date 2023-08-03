using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.ValueObjects
{
    public sealed class InquiryId
    {
        Guid Id { get; set; }
        public InquiryId()
        {
            Id = Guid.NewGuid();
        }
    }
}
