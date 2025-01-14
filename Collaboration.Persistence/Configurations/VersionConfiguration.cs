using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collaboration.Persistence.Configurations;

public class VersionConfiguration : IEntityTypeConfiguration<Domain.Entities.Version>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Version> builder)
    {
        builder.HasKey(e => e.Id)
               .HasName("PK__T_Versio__3214EC070AE9C3DD");

        builder.ToTable("T_Version");

        builder.Property(e => e.Name)
               .IsUnicode(false);

        builder.HasOne(d => d.Account)
               .WithMany(p => p.Versions)
               .HasForeignKey(d => d.AccountId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__T_Version__Accou__5165187F");
    }
}
