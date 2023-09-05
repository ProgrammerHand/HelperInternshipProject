namespace Helper.Core.Offer.Policies
{
    public class Percents10Policy : IDiscounter
    {
        public double CalculateDiscount(double price)
        {
            return price*0.9;
        }
    }
}
