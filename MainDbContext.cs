using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HotelBackend
{
    public class MainDbContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Enable Geospatial Extensions
            modelBuilder.HasPostgresExtension("postgis");

            // Add spatial index for optimized spatial queries
            // TODO: this can probably be done in a Model file as an annotation,
            // I just haven't have time to find out how to do it
            modelBuilder.Entity<Hotel>()
                .HasIndex(h => h.GeoLocation)
                .HasMethod("GIST");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Not for production!
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") {
                //optionsBuilder
                    //.EnableSensitiveDataLogging()
                    //.LogTo(Console.WriteLine); 
            }
        }
    }
}