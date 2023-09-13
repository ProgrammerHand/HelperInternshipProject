namespace Helper.Application.Integrations
{
    public interface IPdfGenerator
    {
        byte[] GenerateOffer(Core.Inquiry.Inquiry Inquiry, Core.Offer.Offer Offer);
    }
}
