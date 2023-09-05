using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.Solution;
using Helper.Core.Solution.ValueObjects;
using Helper.Core.User.Value_objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Helper.Infrastructure.DAL.Configurations
{
    internal sealed class SolutionConfiguration : IEntityTypeConfiguration<Solution>
    {
        public void Configure(EntityTypeBuilder<Solution> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new SolutionId(x));
            builder.Property(x => x.InquiryId).HasConversion(x => x.Value, x => new InquiryId(x));
            builder.Property(x => x.Description).HasConversion(x => x.Value, x => new Description(x));
            builder.Property(x => x.Variant).HasConversion(x => x.Value, x => new SolutionVariant(x));
            builder.Property(x => x.AssignedConsultant).HasConversion(x => x.Value, x => new UserId(x));
            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.OwnsOne(x => x.RequestedCompletionDate, cd =>
            {
                cd.Property(x => x.Start).HasColumnName("RequestedStartDate");
                cd.Property(x => x.End).HasColumnName("RequestedEndDate");
            });
        }
    }
}
