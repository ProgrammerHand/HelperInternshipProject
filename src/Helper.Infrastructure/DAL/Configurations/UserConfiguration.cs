using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.User;
using Helper.Core.User.Value_objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Infrastructure.DAL.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) 
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new UserId(x));
            builder.Property(x => x.Email).HasConversion(x => x.Value, x => new UserEmail(x)).IsRequired();
            builder.Property(x => x.PasswordHash).HasConversion(x => x.Value, x => new UserPassword(x)).IsRequired();
            builder.Property(x => x.Role).HasConversion(x => x.Value, x => new UserRole(x)).IsRequired();
            builder.Property(x => x.RowVersion).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate().IsRequired();
        }
    }
}
