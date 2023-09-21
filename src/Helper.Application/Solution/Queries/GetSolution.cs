using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;

namespace Helper.Application.Solution.Queries
{
    public class GetSolution : IQuery<SolutionDto>
    {
        public Guid Id { get; private set; }

        public GetSolution(Guid id)
        {
            Id = id;
        }
    }
}
