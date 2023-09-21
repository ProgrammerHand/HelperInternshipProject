using Helper.Core.Exceptions;

namespace Helper.Infrastructure.Integrations.Exceptions
{
    public sealed class ReservationCollisionException : CustomException
    {
        public ReservationCollisionException() : base("Given range collides with other reservation")
        {
        }
    }
}
