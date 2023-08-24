using Helper.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.Offer.Exceptions
{
    public class OfferNotVerifiedException : CustomException
    {
        public OfferNotVerifiedException() : base("Offer not verified yet")
        {
            
        }
    }
}
