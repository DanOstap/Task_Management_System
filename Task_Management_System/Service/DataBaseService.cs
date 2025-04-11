using Microsoft.EntityFrameworkCore;
using Task_Management_System.Models;

namespace Task_Management_System.Service;

public class DataBaseService : DbContext
{
    public DbSet<Users> Users { get; set; }
    public DbSet<Tasks> Tasks { get; set; }
    
    public DataBaseService(DbContextOptions<DataBaseService> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Users>()
                                    .HasMany( e => e.Tasks )
                                    .WithMany( e => e.Users );
    }
}