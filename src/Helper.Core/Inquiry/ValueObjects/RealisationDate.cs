﻿using Helper.Core.Inquiry.Exceptions;
using Helper.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.Inquiry.ValueObjects
{
    public sealed record RealisationDate
    {
        public DateTime Start { get; }
        public DateTime? End { get; }

        public RealisationDate(DateTime StartDate, DateTime? EndDate, Variants Variant, IClockCustom clock)
        {
            if (Variant == Variants.consulting && EndDate is null)
            {
                throw new NotGivenConsaltingEndException();
            }

            if (EndDate < StartDate)
            {
                throw new EndBeforeStartException();
            }

            if (StartDate < clock.Now.AddDays(7))
            {
                throw new StartDateTooEarly();
            }
            Start = StartDate;
            End = EndDate;
        }
        private RealisationDate() 
        { }

    }
}
