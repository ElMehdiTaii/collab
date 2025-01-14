using Collaboration.Domain.Entities;
using Collaboration.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Collaboration.Persistence.DatabaseContext;

public class CollaborationDatabaseContext : DbContext
{
    public CollaborationDatabaseContext(DbContextOptions<CollaborationDatabaseContext> options) : base(options)
    {

    }
    public DbSet<User> User { get; set; }
    public DbSet<Account> Account { get; set; }
    public DbSet<Document> Document { get; set; }
    public DbSet<DocumentNote> DocumentComment { get; set; }
    public DbSet<Folder> Folder { get; set; }
    public DbSet<FolderNote> FolderComment { get; set; }
    public DbSet<Tag> Tag { get; set; }
    public DbSet<Domain.Entities.Version> Version { get; set; }
    public DbSet<Board> Board { get; set; }
    public DbSet<Domain.Entities.Task> Task { get; set; }
    public DbSet<TaskAttachement> TaskAttachement { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CollaborationDatabaseContext).Assembly);
        modelBuilder.ApplyConfiguration(new BoardConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentConfiguration());
        //modelBuilder.ApplyConfiguration(new DocumentNoteConfiguration());
        modelBuilder.ApplyConfiguration(new FolderConfiguration());
        modelBuilder.ApplyConfiguration(new FolderNoteConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new VersionConfiguration());
        modelBuilder.ApplyConfiguration(new TaskConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}
