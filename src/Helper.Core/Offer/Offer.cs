using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.Offer.ValueObjects;
using Helper.Infrastructure.DAL;

namespace Helper.Core.Offer
{
    public class Offer : ISoftDelete
    {
        public OfferId Id { get; private set; }
        public InquiryId PrecursorId { get; private set; }
        public Inquiry.Inquiry Precursor { get; private set; }
        public OfferDescription Description { get; private set; }
        public byte[] RowVersion { get; private set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

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
