using Collaboration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collaboration.Persistence.Configurations;

public class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__T_Board__3214EC072F6AF432");

        builder.ToTable("T_Board");

        builder.HasOne(d => d.Account).WithMany(p => p.Boards)
            .HasForeignKey(d => d.AccountId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__T_Board__Account__29221CFB");
    }
}
