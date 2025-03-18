using Microsoft.EntityFrameworkCore;
using TourismAgencyAPI.Models;
using TourismAgencyAPI.Data.Configurations;

namespace TourismAgencyAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TourismPackage> TourismPackages { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TourismPackageConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        }
    }
}