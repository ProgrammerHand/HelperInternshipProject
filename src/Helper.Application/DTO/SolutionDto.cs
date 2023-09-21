namespace Helper.Application.DTO
{
    public sealed record SolutionDto
    {
        public Guid Id { get; set; }
        public Guid InquiryId { get; set; }
        public string Description { get; set; }
        public DateTime RealistationStart { get; set; }
        public DateTime? RealisationEnd { get; set; }
        public string Variant { get; set; }
        public Guid? AssignedConsultant { get; set; }
        public Guid? AssignedTime { get; set; }
    }
}
