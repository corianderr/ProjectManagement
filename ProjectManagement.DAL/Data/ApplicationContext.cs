using Microsoft.EntityFrameworkCore;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.DAL.Data;

public class ApplicationContext : DbContext
{
    public DbSet<Project>? Projects { get; set; }
    public DbSet<Project>? Employees { get; set; }
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
}