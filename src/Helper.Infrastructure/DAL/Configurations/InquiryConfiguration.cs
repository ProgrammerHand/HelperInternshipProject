using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Helper.Infrastructure.DAL.Configurations
{
    internal sealed class InquiryConfiguration : IEntityTypeConfiguration<Inquiry>
    {
        public void Configure(EntityTypeBuilder<Inquiry> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new InquiryId(x));
            builder.Property(x => x.Description).HasConversion(x => x.Value, x => new Description(x)).IsRequired();

            builder.Property(x => x.FeasibilityNote).HasConversion(x => x.Value, x => new FeasibilityNote(x));
            builder.Property(x => x.AcceptanceStatus).HasConversion(x => x.Value, x => new AcceptanceStatus(x));
            builder.Property(x => x.SolutionDecision).HasConversion(x => x.Value, x => new SolutionVariant(x)).IsRequired();

            //builder.Property(x => x.RequestedCompletionDate).HasConversion(x => (x.Start, x.End), x => new RealisationDate(x.Start, x.End)).IsRequired();
            builder.OwnsOne(
                x => x.RequestedCompletionDate,
                cd =>
                {
                    cd.Property(x => x.Start).HasColumnName("RequestedStartDate");
                    cd.Property(x => x.End).HasColumnName("RequestedEndDate");
                });

            builder.HasOne(x => x.Author).WithMany(x => x.Inquiries);
        }
    }
}
