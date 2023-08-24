using Helper.Core.Exceptions;

namespace Helper.Core.Offer.Exceptions
{
    public class InccorectPriceException : CustomException
    {
        public InccorectPriceException() : base("Price is <= 0")
        {
        }
    }
}
