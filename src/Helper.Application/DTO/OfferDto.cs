namespace Helper.Application.DTO
{
    public sealed record OfferDto
    {
        public Guid Id { get; set; }
        public Guid InquiryId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool IsDraft { get; set; }
        public bool IsVerified { get; set; }
        public string Status { get; set; }
        public string ClientsReason { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
