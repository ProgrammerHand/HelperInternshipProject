using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.Offer.ValueObjects;
using Helper.Core.Utility;
using Helper.Infrastructure.DAL;

namespace Helper.Core.Offer
{
    public class Offer : ISoftDelete, IRowVersionControl
    {
        public OfferId Id { get; private set; }
        public InquiryId InquiryId { get; private set; }
        public OfferDescription Description { get; private set; }
        public OfferPrice Price { get; private set; }
        public bool IsDraft { get; private set; }
        public bool IsVerified { get; private set; }
        public Status Status { get; private set; } = Status.awaits_decision;
        public OfferRejectReason? ClientsReason { get; private set; }
        public byte[] RowVersion { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        private Offer(OfferId id, Inquiry.Inquiry inquiry)
        {
            Id = id;
            InquiryId = inquiry.Id;
            Description = inquiry.FeasibilityNote.Value;
        }

        private Offer()
        {
        }

        public static Offer CreateOffer( Inquiry.Inquiry precursor)
        {
            var id = Guid.NewGuid();
            return new Offer(id, precursor);
        }

        public void Accept() 
        {
            Status = Status.accepted;
        }

        public void Reject(string clientsReason)
        {
            if (ClientsReason is not null)
            {
                ClientsReason.Extend(clientsReason);
            }
            else
            {
                ClientsReason = new OfferRejectReason(clientsReason);
            }
        }

        public void SpecifyPrice()
        {
        }

    }
}
