using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.Offer.Exceptions;
using Helper.Core.Offer.ValueObjects;
using Helper.Core.User.Value_objects;
using Helper.Core.Utility;
using Helper.Infrastructure.DAL;

namespace Helper.Core.Offer
{
    public class Offer : ISoftDelete, IRowVersionControl, IDataAudit
    {
        public OfferId Id { get; private set; }
        public InquiryId InquiryId { get; private set; }
        public UserId CustomerId { get; private set; }
        public OfferDescription Description { get; private set; }
        public OfferPrice? Price { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public SolutionCloudStorage? SolutionStorage { get; private set; }
        public DateTime RealisationStartDate { get; private set; }
        public bool IsDraft { get; private set; } = true;
        public bool IsVerified { get; private set; } = false;
        public AcceptanceStatus Status { get; private set; } = new(Inquiry.ValueObjects.Status.awaits_decision);
        public OfferRejectReason? ClientsReason { get; private set; }
        public byte[] RowVersion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        private Offer(OfferId id, Inquiry.Inquiry inquiry)
        {
            Id = id;
            InquiryId = inquiry.Id;
            CustomerId = inquiry.Author.Id;
            Description = inquiry.FeasibilityNote.Value;
            RealisationStartDate = inquiry.RequestedCompletionDate.Start;
        }

        private Offer()
        {
        }

        public static Offer CreateOffer(Inquiry.Inquiry inquiry)
        {
            var id = Guid.NewGuid();
            return new Offer(id, inquiry);
        }

        public void Accept() 
        {
            if(Status.Value is not Inquiry.ValueObjects.Status.awaits_decision)
                throw new OfferDecisionAlredyGivenException();
            if (IsDraft is true)
                throw new OfferNotReadyException();
            Status = Inquiry.ValueObjects.Status.accepted;
        }

        public void Reject(string clientsReason)
        {
            if (IsDraft is true)
                throw new OfferNotReadyException();
            if (Status.Value is not Inquiry.ValueObjects.Status.awaits_decision)
                throw new OfferDecisionAlredyGivenException();

            if (ClientsReason is not null)
            {
                ClientsReason.Extend(clientsReason);
            }
            else
            {
                ClientsReason = new OfferRejectReason(clientsReason);
            }
        }

        public void AddSolutionStorage(string link) 
        {
            SolutionStorage = new SolutionCloudStorage(link);
        }

        public void AddPaymentDate(DateTime date) 
        {
            if ((RealisationStartDate - date).Days < 5)
                throw new PaymentDateTooLateException();
                    PaymentDate = date;
        }

        public void SpecifyPrice(double price)
        {
            if (PaymentDate is null)
                throw new NoPaymentDateException();
            Price = new OfferPrice(price);
        }

        public void FinalizeDraft()
        {
            if (PaymentDate is null)
                throw new NoPaymentDateException();
            if (Price is null)
                throw new InccorectPriceException();
            IsDraft = false;
        }

    }
}
