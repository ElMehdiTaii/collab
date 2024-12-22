using Collaboration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collaboration.Persistence.Configurations;

internal class FolderConfiguration : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder.ToTable("T_Folder");

        builder.HasKey(e => e.Id)
               .HasName("PK__T_Folder__3214EC0706F93BBE");

        builder.Property(e => e.CreatedAt)
               .HasColumnType("datetime");

        builder.HasOne(d => d.Account)
               .WithMany(p => p.TFolders)
               .HasForeignKey(d => d.AccountId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__T_Folder__Accoun__571DF1D5");

        builder.HasOne(d => d.FolderMain)
               .WithMany(p => p.InverseFolderMain)
               .HasForeignKey(d => d.FolderMainId)
               .HasConstraintName("FK__T_Folder__Folder__5629CD9C");

        builder.HasOne(d => d.Tag)
               .WithMany(p => p.TFolders)
               .HasForeignKey(d => d.TagId)
               .HasConstraintName("FK__T_Folder__TagId__5535A963");

        builder.HasOne(d => d.Version)
               .WithMany(p => p.TFolders)
               .HasForeignKey(d => d.VersionId)
               .HasConstraintName("FK__T_Folder__Versio__5441852A");
    }
}
