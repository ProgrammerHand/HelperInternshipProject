namespace Helper.Core.Offer.Policies
{
    public interface IDiscounter
    {
        double CalculateDiscount(double price);
    }
}
