using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;

namespace Helper.Application.ReservedEmployeeTime.Queries
{
    public class GetAvailableEmployee : IQuery<List<UserDto>>
    {
        public DateTime RealisationStart { get; private set; }
        public DateTime RealisationEnd { get; private set; }

        public GetAvailableEmployee(DateTime realisationStart, DateTime realisationEnd)
        {
            RealisationStart = realisationStart;
            RealisationEnd = realisationEnd;
        }
    }
}
