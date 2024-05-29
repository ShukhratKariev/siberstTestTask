using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Employee> Employees { get; set; }
    
    public DbSet<Project> Projects { get; set; }
    
    public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
    
    public DbSet<Ticket> Tickets { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}