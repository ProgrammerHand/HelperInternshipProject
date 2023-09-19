using Helper.Core.ReservedEmployeeTime;
using Helper.Core.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Helper.Infrastructure.DAL.Configurations
{
    internal sealed class ReservedEmployeeTimeConfiguration : IEntityTypeConfiguration<ReservedEmployeeTime>
    {
        public void Configure(EntityTypeBuilder<ReservedEmployeeTime> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new ReservedEmployeeTimeId(x));
            builder.Property(x => x.WorkerId).HasConversion(x => x.Value, x => new UserId(x));
            builder.Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
