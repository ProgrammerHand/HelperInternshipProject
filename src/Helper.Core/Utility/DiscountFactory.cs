using Helper.Application.Abstraction;
using Helper.Core.Offer.Policies;

namespace Helper.Core.Utility
{
    public static class DiscountFactory
    {
        public static IDiscounter CreateDiscount(IClockCustom clock)
        {
            var condition = clock.Now.Month&2;
            return condition switch
            {
                0 => new Percents20Policy(),
                1 => new Percents10Policy(),
                _ => new NoPercentsPolicy()
            };
        }
    }
}
