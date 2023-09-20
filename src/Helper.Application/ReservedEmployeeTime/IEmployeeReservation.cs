using Helper.Application.DTO;
using Helper.Core.ReservedEmployeeTime;

namespace Helper.Application.ReservedEmployeeTime
{
    public interface IEmployeeReservation
    {
        public Task<List<UserDto>> GetAvailableEmployee(Guid offerId);
        public Task<ReservedEmployeeTimeId> ReserveEmployee(Guid userId, DateTime RealisationStart, DateTime? RealisationEnd);
    }
}
