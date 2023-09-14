namespace Helper.Application.Integrations
{
    public interface IPdfGenerator
    {
        public byte[] GenerateOffer(Core.Inquiry.Inquiry Inquiry, Core.Offer.Offer Offer);
    }
}
