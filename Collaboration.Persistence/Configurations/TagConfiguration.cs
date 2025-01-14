using Collaboration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collaboration.Persistence.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("T_Tag");

        builder.HasKey(e => e.Id)
               .HasName("PK__T_Tag__3214EC072FA44186");

        builder.Property(e => e.Name)
               .IsUnicode(false);

        builder.HasOne(d => d.Account)
               .WithMany(p => p.Tags)
               .HasForeignKey(d => d.AccountId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__T_Tag__AccountId__4E88ABD4");
    }
}
