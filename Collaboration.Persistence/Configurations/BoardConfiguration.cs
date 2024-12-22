using Collaboration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collaboration.Persistence.Configurations;

public class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder.ToTable("T_Board");

        builder.HasKey(e => e.Id)
               .HasName("PK__T_Board__3214EC071BE2DA0F");

        builder.HasOne(d => d.Account)
               .WithMany(p => p.TBoards)
               .HasForeignKey(d => d.AccountId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__T_Board__Account__6383C8BA");
    }
}
