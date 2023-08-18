using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.Offer.ValueObjects;

namespace Helper.Core.Offer
{
    public class Offer
    {
        public OfferId Id { get; private set; }
        public InquiryId PrecursorId { get; private set; }
        public Inquiry.Inquiry Precursor { get; private set; }
        public OfferDescription Description { get; private set; }
        public byte[] RowVersion { get; private set; }

        private Offer(OfferId id, Inquiry.Inquiry precursor)
        {
            Id = id;
            PrecursorId = precursor.Id;
            Precursor = precursor;
            Description = precursor.FeasibilityNote.Value;
        }

        private Offer()
        {
        }

        public static Offer CreateOffer( Inquiry.Inquiry precursor)
        {
            var id = Guid.NewGuid();
            return new Offer(id, precursor);
        }
            
    }
}
