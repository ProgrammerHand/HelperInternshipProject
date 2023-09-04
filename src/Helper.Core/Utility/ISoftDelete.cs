namespace Helper.Infrastructure.DAL
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; }
        public DateTime? DeletedAt { get; }
    }
}
