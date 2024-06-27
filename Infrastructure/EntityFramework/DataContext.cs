using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework;

public class DataContext : DbContext
{
    DbSet<User> Users { get; set; }
    
    public DataContext() {}

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>()
            .HasIndex(x => x.Login)
            .IsUnique();
    }
}