using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;
using Helper.Core.User;

namespace Helper.Application.Solution.Queries
{
    public class GetOwnedSolutions : IQuery<List<SolutionDto>>
    {
        public UserId WorkerId { get; private set; }

        public GetOwnedSolutions(Guid id)
        {
            WorkerId = id;
        }
    }
}
