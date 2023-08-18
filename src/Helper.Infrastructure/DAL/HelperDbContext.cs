using Helper.Core.Inquiry;
using Helper.Core.Offer;
using Helper.Core.User;
using Microsoft.EntityFrameworkCore;

namespace Helper.Infrastructure.DAL
{
    public sealed class HelperDbContext : DbContext
    {
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public HelperDbContext(DbContextOptions<HelperDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
