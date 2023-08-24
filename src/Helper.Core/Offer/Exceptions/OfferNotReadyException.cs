using Helper.Core.Exceptions;

namespace Helper.Core.Offer.Exceptions
{
    public class OfferNotReadyException : CustomException
    {
        public OfferNotReadyException() : base("Offer not ready")
        {
        }
    }
}
