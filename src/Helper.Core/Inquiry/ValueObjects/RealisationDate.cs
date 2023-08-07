using Helper.Core.Inquiry.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.Inquiry.ValueObjects
{
    public sealed record RealisationDate
    {
        public DateTime Start { get; private set; }
        public DateTime? End { get; private set; }

        public RealisationDate(DateTime StartDate, DateTime? EndDate, Variants Variant)
        {
            if (Variant == Variants.consulting && EndDate is null)
            {
                throw new NotGivenConsaltingEndException();
            }
            Start = StartDate;
            End = EndDate;
        }
        private RealisationDate() 
        { }

    }
}
