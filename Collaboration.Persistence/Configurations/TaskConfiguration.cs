using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collaboration.Persistence.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__T_TASK__3214EC07E441509D");

        builder.ToTable("T_TASK");

        builder.Property(e => e.Description).IsUnicode(false);
        builder.Property(e => e.EndDate).HasColumnType("datetime");
        builder.Property(e => e.StartDate).HasColumnType("datetime");
        builder.Property(e => e.Title).IsUnicode(false);

        builder.HasOne(d => d.Board).WithMany(p => p.Tasks)
            .HasForeignKey(d => d.BoardId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__T_TASK__BoardId__2CF2ADDF");

        builder.HasOne(d => d.User).WithMany(p => p.Tasks)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__T_TASK__UserId__2BFE89A6");
    }
}
