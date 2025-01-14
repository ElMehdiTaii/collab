using Collaboration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collaboration.Persistence.Configurations;

public class FolderNoteConfiguration : IEntityTypeConfiguration<FolderNote>
{
    public void Configure(EntityTypeBuilder<FolderNote> builder)
    {
        builder.ToTable("T_FolderNote");
        
        builder.HasKey(e => e.Id).HasName("PK__T_Folder__3214EC072D808287");

        builder.Property(e => e.CreatedAt).HasColumnType("datetime");

        builder.HasOne(d => d.CreatedByNavigation).WithMany(p => p.FolderNotes)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__T_FolderN__Creat__59FA5E80");

        builder.HasOne(d => d.Folder).WithMany(p => p.FolderNotes)
            .HasForeignKey(d => d.FolderId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__T_FolderN__Folde__5AEE82B9");
    }
}
