using Collaboration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collaboration.Persistence.Configurations;

public class DocumentNoteConfiguration : IEntityTypeConfiguration<DocumentNote>
{
    public void Configure(EntityTypeBuilder<DocumentNote> builder)
    {
        throw new NotImplementedException();
    }
}
