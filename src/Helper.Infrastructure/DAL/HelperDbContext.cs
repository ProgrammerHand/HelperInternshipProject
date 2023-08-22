using Helper.Core;
using Helper.Core.Inquiry;
using Helper.Core.Offer;
using Helper.Core.User;
using Helper.Core.Utility;
using Helper.Infrastructure.DAL.Exceptions;
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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            CheckRowVersion();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void CheckRowVersion()
        {
            ChangeTracker.DetectChanges();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IRowVersionControl)
                {
                    var property = entry.Property("RowVersion");
                    if (property.OriginalValue.Equals(property.CurrentValue))
                        throw new WrongRowVersionException();
                }
            }

        }
    }
}
