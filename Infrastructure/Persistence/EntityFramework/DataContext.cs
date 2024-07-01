using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EntityFramework;

public class DataContext : DbContext
{
    DbSet<User> Users { get; set; }
    
    public DataContext(DbContextOptions<DataContext> options) 
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>()
            .Property(x => x.Id).HasColumnName("id");
        modelBuilder.Entity<Role>()
            .Property(x => x.Name).HasColumnName("name");
        
        modelBuilder.Entity<User>()
            .Property(x => x.Id).HasColumnName("id");
        modelBuilder.Entity<User>()
            .Property(x => x.RoleId).HasColumnName("role_id");
        modelBuilder.Entity<User>()
            .Property(x => x.Login).HasColumnName("login");
        modelBuilder.Entity<User>()
            .Property(x => x.Password).HasColumnName("password");
        modelBuilder.Entity<User>()
            .Property(x => x.Name).HasColumnName("name");
        modelBuilder.Entity<User>()
            .Property(x => x.IsDeleted).HasColumnName("is_deleted");
        
        modelBuilder
            .Entity<User>()
            .HasIndex(x => x.Login)
            .IsUnique();
    }
}