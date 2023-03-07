using Microsoft.EntityFrameworkCore;
using ProjectManagement.DAL.Models;
using ProjectManagement.DAL.Models.Common;

namespace ProjectManagement.DAL.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Project> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Manager)
            .WithMany(b => b.ManagedProjects);
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.WorkedOnProjects)
            .WithMany(p => p.ExecutiveEmployees);
    }

    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<IDateFixEntity>())
            entry.Entity.StartDate = entry.State switch
            {
                EntityState.Added => DateTime.Now,
                _ => entry.Entity.StartDate
            };
        return await base.SaveChangesAsync(cancellationToken);
    }
}