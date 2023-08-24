namespace Helper.Application.Abstraction
{
    public interface IDiscounter
    {
        double CalculateDiscount(double price, DateTime UserCreationTime);
    }
}
