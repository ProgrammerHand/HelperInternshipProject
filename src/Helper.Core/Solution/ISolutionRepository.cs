namespace Helper.Core.Solution
{
    public interface ISolutionRepository
    {
        Task AddAsync(Solution solution);
        Task UpdateAsync(Solution solution);
        Task<Solution> GetByIdAsync(SolutionId solution);
    }
}
