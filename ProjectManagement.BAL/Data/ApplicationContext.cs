using Microsoft.EntityFrameworkCore;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BAL.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Project>? Projects { get; set; }
    public DbSet<Project>? Employees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Manager)
            .WithMany(b => b.ManagedProjects);
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.WorkedOnProjects)
            .WithMany(p => p.ExecutiveEmployees);
    }
}