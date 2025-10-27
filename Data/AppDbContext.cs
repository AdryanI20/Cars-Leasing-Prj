using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace AutoLeasingApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // Accept options from DI
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<LeaseContract> LeaseContracts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeaseContract>()
                .HasOne(lc => lc.User)
                .WithMany(u => u.LeaseContracts)
                .HasForeignKey(lc => lc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LeaseContract>()
                .HasOne(lc => lc.Car)
                .WithMany(c => c.LeaseContracts)
                .HasForeignKey(lc => lc.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Car>()
                .Property(c => c.IsAvailable)
                .HasDefaultValue(true);

            modelBuilder.Entity<Car>()
                .Property(c => c.DailyRate)
                .HasPrecision(18, 2);

            modelBuilder.Entity<LeaseContract>()
                .Property(lc => lc.TotalCost)
                .HasPrecision(18, 2);
        }
    }
}