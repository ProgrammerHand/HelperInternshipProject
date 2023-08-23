using Helper.Core.Exceptions;

namespace Helper.Core.Offer.Exceptions
{
    public class InccorectPrice : CustomException
    {
        public InccorectPrice() : base("Price is 0 or lower")
        {
        }
    }
}
