using Helper.Application.Abstraction;
using Helper.Core;

namespace Helper.Infrastructure
{
    public class Discounter : IDiscounter
    {
        private readonly IClockCustom _clock;

        public Discounter(IClockCustom clock)
        {
            _clock = clock;
        }
        public double CalculateDiscount(double price, DateTime UserCreationTime) 
        {
            if (_clock.Now.Day % 2 == 0)
                price -= 100;
            if ((UserCreationTime - _clock.Now) > new TimeSpan(8765, 40, 0))
                price -= 200;
            return price;
        }
    }
}
