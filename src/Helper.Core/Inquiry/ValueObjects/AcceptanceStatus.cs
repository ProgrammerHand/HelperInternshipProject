using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.Inquiry.ValueObjects
{
    public sealed record AcceptanceStatus
    {
        public Status Value { get; }

        public AcceptanceStatus(Status status)
        {
            Value = status;
        }

        public static implicit operator Status(AcceptanceStatus data) => data.Value;

        public static implicit operator AcceptanceStatus(Status value) => new(value);
    }
}
