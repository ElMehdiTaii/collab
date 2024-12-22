using Collaboration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collaboration.Persistence.Configurations;

internal class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("T_Document");

        builder.HasKey(e => e.Id)
               .HasName("PK__T_Docume__3214EC07F05AE5D7");

        builder.Property(e => e.CreatedAt)
               .HasColumnType("datetime");

        builder.Property(e => e.ExpiryDate)
               .HasColumnType("datetime");

        builder.Property(e => e.Path)
               .IsUnicode(false);

        builder.Property(e => e.Type)
                .IsUnicode(false);

        builder.HasOne(d => d.Account)
               .WithMany(p => p.TDocuments)
               .HasForeignKey(d => d.AccountId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__T_Documen__Accou__60A75C0F");

        builder.HasOne(d => d.Folder)
               .WithMany(p => p.TDocuments)
               .HasForeignKey(d => d.FolderId)
               .HasConstraintName("FK__T_Documen__Folde__5FB337D6");

        builder.HasOne(d => d.Tag)
               .WithMany(p => p.TDocuments)
               .HasForeignKey(d => d.TagId)
               .HasConstraintName("FK__T_Documen__TagId__5EBF139D");

        builder.HasOne(d => d.Version)
               .WithMany(p => p.TDocuments)
               .HasForeignKey(d => d.VersionId)
               .HasConstraintName("FK__T_Documen__Versi__5DCAEF64");
    }
}
