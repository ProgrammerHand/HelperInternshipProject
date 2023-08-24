using Helper.Core.Exceptions;

namespace Helper.Core.Offer.Exceptions
{
    public sealed class OfferDecisionAlredyGivenException : CustomException
    {
        public OfferDecisionAlredyGivenException() : base("Offer decision alredy given")
        {

        }
    }
}
