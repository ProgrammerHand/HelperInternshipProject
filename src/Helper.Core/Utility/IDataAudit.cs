namespace Helper.Core.Utility
{
    public interface IDataAudit
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
