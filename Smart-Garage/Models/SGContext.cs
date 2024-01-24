using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Numerics;

namespace Smart_Garage.Models
{
    public class SGContext : DbContext
    {
        public SGContext(DbContextOptions<SGContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            
            //Vehicle
            modelBuilder.Entity<Vehicle>()
                .HasKey(v => v.Id);

            //Service
            modelBuilder.Entity<Service>()
                .HasKey(s => s.Id);

            base.OnModelCreating(modelBuilder);

        }
    }
}
