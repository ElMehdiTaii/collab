using Collaboration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Version = Collaboration.Domain.Entities.Version;

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
    public DbSet<FolderComment> FolderComment { get; set; }
    public DbSet<Tag> Tag { get; set; }
    public DbSet<Version> Version { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CollaborationDatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
        //    .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        //{
        //    entry.Entity.DateModified = DateTime.Now;
        //    entry.Entity.ModifiedBy = _userService.UserId;
        //    if (entry.State == EntityState.Added)
        //    {
        //        entry.Entity.DateCreated = DateTime.Now;
        //        entry.Entity.CreatedBy = _userService.UserId;
        //    }
        //}
        return base.SaveChangesAsync(cancellationToken);
    }
}
