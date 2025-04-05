using Collaboration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collaboration.Persistence.Configurations
{
    public class TaskAttachementConfiguration : IEntityTypeConfiguration<TaskAttachement>
    {
        public void Configure(EntityTypeBuilder<TaskAttachement> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__T_TASK_A__3214EC0789546766");

            builder.ToTable("T_TASK_ATTACHEMENT");

            builder.Property(e => e.ContentType).HasMaxLength(100);
            builder.Property(e => e.Name).HasMaxLength(255);

            builder.HasOne(d => d.Task).WithMany(p => p.TaskAttachements)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__T_TASK_AT__TaskI__2FCF1A8A");
        }
    }
}
