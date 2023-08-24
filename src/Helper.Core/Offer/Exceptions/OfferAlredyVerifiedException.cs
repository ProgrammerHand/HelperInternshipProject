using Helper.Core.Exceptions;

namespace Helper.Core.Offer.Exceptions
{
    public class OfferAlredyVerifiedException : CustomException
    {
        public OfferAlredyVerifiedException() : base("Offer already verified")
        {

        }
    }
}
