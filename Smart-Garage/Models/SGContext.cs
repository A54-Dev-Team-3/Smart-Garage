using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Numerics;

namespace Smart_Garage.Models
{
    public class SGContext : DbContext
    {
        public SGContext(DbContextOptions<SGContext> options): base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<VehicleService> VehicleService { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //User
            modelBuilder.Entity<User>(e =>
            {
                e.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(20);
                
                e.Property(u => u.PasswordHash)
                .IsRequired();

                e.Property(u => u.PasswordSalt)
                .IsRequired();

                e.Property(u => u.Email)
                .IsRequired();

                e.Property(u => u.PhoneNumber)
                .IsRequired()
                .HasMaxLength(10);
            });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();


            //Vehicle
            modelBuilder.Entity<Vehicle>(e =>
            {
                e.Property(v => v.LicensePlate)
                .IsRequired();

                e.Property(v => v.VIN)
                .IsRequired()
                .HasMaxLength(17)
                .HasColumnType("nvarchar(17)");

                e.Property(v => v.CreationYear)
                .IsRequired()
                .HasColumnType("int");

                e.HasCheckConstraint("CK_CreationYear", "[CreationYear] > 1867");

                e.Property(v => v.Model)
                .IsRequired()
                .HasMaxLength(50)
                .HasAnnotation("MinLength", 2);

                e.Property(v => v.Brand)
                .IsRequired()
                .HasMaxLength(50)
                .HasAnnotation("MinLength", 2);

            });
            

            // Service
            modelBuilder.Entity<Service>(e =>
            {
                e.Property(s => s.Name)
                .IsRequired();

                e.Property(s => s.Price)
                .IsRequired();
            });

            // VehicleService
            modelBuilder.Entity<VehicleService>()
                .HasKey(vs => new { vs.ServiceId, vs.VehicleId });

            modelBuilder.Entity<VehicleService>()
                .HasOne(vs => vs.Vehicle)
                .WithMany(v => v.VehicleServices)
                .HasForeignKey(vs => vs.VehicleId);

            modelBuilder.Entity<VehicleService>()
                .HasOne(vs => vs.Service)
                .WithMany(s => s.VehicleServices)
                .HasForeignKey(vs => vs.ServiceId);
        }
    }
}
