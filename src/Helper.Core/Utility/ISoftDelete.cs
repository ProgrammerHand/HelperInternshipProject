namespace Helper.Infrastructure.DAL
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public void Restore()
        {
            IsDeleted = false;
            DeletedAt = null;
        }
    }
}
