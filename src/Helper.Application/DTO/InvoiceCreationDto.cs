namespace HelperPayment.Core.DTO
{
    public sealed record InvoiceCreationDto
    {
        public Guid OfferId { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime RealisationStart { get; set; }
        public DateTime RealisationEnd { get; set;}
        public double Price { get; set; }
        public string ClientEmail { get; set; }
        public string ClientName { get; set; }
    }
}
