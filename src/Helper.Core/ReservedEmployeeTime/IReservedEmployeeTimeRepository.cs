namespace Helper.Core.ReservedEmployeeTime
{
    public interface IReservedEmployeeTimeRepository
    {
        Task AddAsync(ReservedEmployeeTime reservation);
        Task UpdateAsync(ReservedEmployeeTime reservation);
        Task<ReservedEmployeeTime> GetByIdAsync(ReservedEmployeeTimeId id);
    }
}
