using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Kullanıcı adı benzersiz olmalı
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        // Email benzersiz olmalı
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Kullanıcı silindiğinde görevleri de silinsin
        modelBuilder.Entity<User>()
            .HasMany(u => u.Tasks)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 