using Helper.Core.Inquiry;
using Helper.Core.Offer;
using Helper.Core.Offer.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Helper.Infrastructure.DAL.Configurations
{
    internal sealed class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new OfferId(x));
            builder.Property(x => x.Description).HasConversion(x => x.Value, x => new OfferDescription(x)).IsRequired();
            builder.Property(x => x.RowVersion).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate().IsRequired();

            builder.HasOne(x => x.Precursor).WithMany().HasForeignKey(x => x.PrecursorId);
        }
    }
}
