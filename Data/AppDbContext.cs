using Microsoft.EntityFrameworkCore;
using BlazorServerApp.Models;

namespace BlazorServerApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AssetsQR> AssetsQRs { get; set; }

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

                // Since AssignedDate and DocumentPath no longer exist, remove those mappings
            });
        }
    }
}
