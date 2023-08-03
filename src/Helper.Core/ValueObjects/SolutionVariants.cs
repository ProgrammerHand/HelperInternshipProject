using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.ValueObjects
{
    public sealed class SolutionVariants
    {
        Variants Variant { get; set; }

        public SolutionVariants(Variants variant)
        {
            Variant = variant;
        }

    }
}
