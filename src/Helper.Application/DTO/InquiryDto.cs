namespace Helper.Application.DTO
{
    public class InquiryDto
    {
        public string Description { get; set; }
        public DateOnly RequestedCompletionDate { get; set; }
        public string SolutionDecision { get; set; }
        public string FeasibilityNote { get; set; }
        public string AcceptanceStatus { get; set; }
    }
}
