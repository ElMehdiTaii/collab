using Collaboration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collaboration.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("T_Account");

        builder.HasKey(e => e.Id)
               .HasName("PK__T_Accoun__3214EC07DC4A6BA0");

        builder.Property(e => e.Name)
               .IsUnicode(false);
    }
}