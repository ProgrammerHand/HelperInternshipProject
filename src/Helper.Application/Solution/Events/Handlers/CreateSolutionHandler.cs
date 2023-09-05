using Helper.Application.Abstraction.Events;
using Helper.Application.Inquiry.Events;
using Helper.Core.Inquiry;
using Helper.Core.Solution;

namespace Helper.Application.Solution.Events.Handlers
{
    //public class CreateSolutionHandler : IEventHandler<InquiryAccepted>
    //{
    //    private readonly IInquiryRepository _inquiryRepo;
    //    private readonly ISolutionRepository _solutionRepo;

    //    public CreateSolutionHandler(IInquiryRepository inquiryRepo, ISolutionRepository solutionRepo)
    //    {
    //        _inquiryRepo = inquiryRepo;
    //        _solutionRepo = solutionRepo;
    //    }

    //    public async Task HandleAsync(InquiryAccepted @event)
    //    {
    //        var inquiry = await _inquiryRepo.GetByIdAsync(@event.inquiryId);
    //        var solution = Core.Solution.Solution.CreateSolution(inquiry);
    //        await _solutionRepo.AddAsync(solution);
    //    }
    //}
}
