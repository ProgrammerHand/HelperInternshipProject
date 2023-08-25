using Helper.Core.Exceptions;

namespace Helper.Core.Offer.Exceptions
{
    public class NoOfferPaymentDateException : CustomException
    {
        public NoOfferPaymentDateException() : base("Offer dont have payment date")
        {
            
        }
    }
}
