using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.ValueObjects
{
    internal class CompletionDate
    {
        public DateOnly RequestedCompletionDate { get; set; }
        public CompletionDate(DateOnly date)
        {
            RequestedCompletionDate = date;
        }
    }
}
