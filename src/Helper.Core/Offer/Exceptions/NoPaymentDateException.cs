using Helper.Core.Exceptions;

namespace Helper.Core.Offer.Exceptions
{
    public class NoPaymentDateException : CustomException
    {
        public NoPaymentDateException() : base("Offer dont have payment date")
        {
            
        }
    }
}
