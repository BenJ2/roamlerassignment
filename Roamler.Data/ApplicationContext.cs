using Microsoft.EntityFrameworkCore;
using Roamler.Data.Entities;
using Roamler.Data.Extensions;

namespace Roamler.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().HasKey(x => x.Id);
            
            modelBuilder.SeedLocations();
        }
    }
}
