using Helper.Core.Inquiry.Exceptions;
using Helper.Core.Offer.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.Solution
{
    public sealed record SolutionId
    {
        public Guid Value { get; }
        public SolutionId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(SolutionId data) => data.Value;

        public static implicit operator SolutionId(Guid value) => new(value);
    }
}
