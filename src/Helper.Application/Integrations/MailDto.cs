namespace Helper.Application.Integrations
{
    public sealed record MailDto
    {
        public required string ReciverEmail { get; set; }
        public required string ReciverName { get; set; }
        public required string Subject { get; set; }
        public required string Content { get; set; }
    }
}
