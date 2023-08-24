using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;
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
            builder.Property(x => x.InquiryId).HasConversion(x => x.Value, x => new InquiryId(x));
            builder.Property(x => x.Description).HasConversion(x => x.Value, x => new OfferDescription(x)).IsRequired();
            builder.Property(x => x.Price).HasConversion(x => x.Value, x => new OfferPrice(x));
            builder.Property(x => x.SolutionStorage).HasConversion(x => x.Value, x => new SolutionCloudStorage(x));
            builder.Property(x => x.ClientsReason).HasConversion(x => x.Value, x => new OfferRejectReason(x));
            builder.Property(x => x.Status).HasConversion(x => x.Value, x => new AcceptanceStatus(x));
            builder.Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
