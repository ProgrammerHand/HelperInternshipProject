using Helper.Core.User;
using Helper.Core.Utility;

namespace Helper.Core.ReservedEmployeeTime
{
    public class ReservedEmployeeTime : IRowVersionControl, ISoftDelete
    {
        public ReservedEmployeeTimeId Id { get; private set; }
        public UserId WorkerId { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime? End { get; private set; }
        public byte[] RowVersion { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        private ReservedEmployeeTime(ReservedEmployeeTimeId id, UserId workerId, DateTime start, DateTime? end)
        {
            Id = id;
            WorkerId = workerId;
            Start = start;
            End = end is null ? start.AddHours(2) : end;
        }

        private ReservedEmployeeTime()
        {
            
        }

        public static ReservedEmployeeTime CreateEmployeeReservation(UserId workerId, DateTime start, DateTime? end)
        {
            ReservedEmployeeTimeId id = Guid.NewGuid();
            return new ReservedEmployeeTime(id, workerId, start, end);
        }
    }

}
