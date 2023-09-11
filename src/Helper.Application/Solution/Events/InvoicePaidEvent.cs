namespace Helper.Application.Solution.Events
{
    public sealed record InvoicePaidEvent
    {
        public Guid OfferId { get; set; }
    }
}
