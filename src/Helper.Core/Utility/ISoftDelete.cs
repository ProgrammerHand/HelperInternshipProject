namespace Helper.Core.Utility
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; }
        public DateTime? DeletedAt { get; }
    }
}
