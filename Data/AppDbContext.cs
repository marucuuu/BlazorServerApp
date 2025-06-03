using Microsoft.EntityFrameworkCore;
using BlazorServerApp.Models;

namespace BlazorServerApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AssetsQR> AssetsQRs { get; set; }
        public DbSet<Users> Users { get; set; }  // <-- Add this line

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetsQR>(entity =>
            {
                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AssetType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AssetCondition)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SubUnit)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Users>(entity =>
                {
                    entity.HasKey(e => e.Id);

                    entity.Property(e => e.EmployeeID)
                        .IsRequired()
                        .HasMaxLength(20);

                    entity.Property(e => e.FirstName)
                        .IsRequired()
                        .HasMaxLength(100);

                    entity.Property(e => e.LastName)
                        .IsRequired()
                        .HasMaxLength(100);

                    entity.Property(e => e.Email)
                        .IsRequired()
                        .HasMaxLength(255);

                    entity.Property(e => e.PasswordHash)
                        .IsRequired()
                        .HasMaxLength(255);  // <- Fixed the misplaced semicolon and chaining

                    entity.Property(e => e.PhoneNumber)
                        .HasMaxLength(20);

                    entity.Property(e => e.Role)
                        .IsRequired()
                        .HasMaxLength(50);
                });
        }
    }
}
