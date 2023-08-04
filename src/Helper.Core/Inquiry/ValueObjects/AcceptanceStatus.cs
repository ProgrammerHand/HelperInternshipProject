using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.Inquiry.ValueObjects
{
    public sealed class AcceptanceStatus
    {
        Status Status { get; set; }

        public AcceptanceStatus(Status status)
        {
            Status = status;
        }

        //public static implicit operator Status(AcceptanceStatus data) => data.Status;

        //public static implicit operator AcceptanceStatus(Guid value) => new(value);
    }
}
