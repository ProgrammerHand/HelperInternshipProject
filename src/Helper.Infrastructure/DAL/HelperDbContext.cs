using Helper.Core;
using Helper.Core.Inquiry;
using Helper.Core.Offer;
using Helper.Core.User;
using Microsoft.EntityFrameworkCore;

namespace Helper.Infrastructure.DAL
{
    public class HelperDbContext : DbContext
    {
        private readonly IClockCustom _clock;
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public HelperDbContext(DbContextOptions<HelperDbContext> options, IClockCustom clock) : base(options)
        {
            _clock = clock;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new SoftDeleteInterceptor(_clock));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
