using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Smart_Garage.Services;
using System.Numerics;

namespace Smart_Garage.Models
{
    public class SGContext : DbContext
    {
        public SGContext(DbContextOptions<SGContext> options): base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Visit> Visits { get; set; }

        
        //public DbSet<ServiceInstanceMechanic> Mechanic { get; set; }
        public DbSet<ServiceInstanceService> Service { get; set; }
        public DbSet<ServiceInstancePart> Part { get; set; }


        // Lists of data
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Part> Parts { get; set; }

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
                .IsRequired()
                .HasMaxLength(50)
                .HasAnnotation("MinLength", 2);

                e.Property(v => v.VIN)
                .IsRequired()
                .HasMaxLength(50)
                .HasAnnotation("MinLength", 2);

                e.Property(v => v.CreationYear)
                .IsRequired()
                .HasMaxLength(50)
                .HasAnnotation("MinLength", 2);
            });

            // Visit
            modelBuilder.Entity<Visit>(e =>
            {

                e.Property(s => s.VehicleId)
                .IsRequired();

                e.Property(s => s.Date)
                .IsRequired();

                e.Property(s => s.TotalPrice)
                .IsRequired();

                e.Property(s => s.ServicesTotalPrice)
                .IsRequired();

                e.Property(s => s.PartsTotalPrice)
                .IsRequired();
            });

            // ServiceInstanceMechanic
            //modelBuilder.Entity<ServiceInstanceMechanic>()
            //    .HasKey(sim => new { sim.ServiceInstanceId, sim.MechanicId });

            //modelBuilder.Entity<ServiceInstanceMechanic>()
            //    .HasOne(sim => sim.ServiceInstance)
            //    .WithMany(si => si.ServiceInstanceMechanics)
            //    .HasForeignKey(sim => sim.ServiceInstanceId);

            //modelBuilder.Entity<ServiceInstanceMechanic>()
            //    .HasOne(sim => sim.Mechanic)
            //    .WithMany(m => m.ServiceInstanceMechanics)
            //    .HasForeignKey(sim => sim.MechanicId);

            // ServiceInstanceService
            modelBuilder.Entity<ServiceInstanceService>()
                .HasKey(sis => new { sis.ServiceInstanceId, sis.ServiceId });

            modelBuilder.Entity<ServiceInstanceService>()
                .HasOne(sis => sis.ServiceInstance)
                .WithMany(si => si.ServiceInstanceServices)
                .HasForeignKey(sis => sis.ServiceInstanceId);

            modelBuilder.Entity<ServiceInstanceService>()
                .HasOne(sis => sis.Service)
                .WithMany(s => s.ServiceInstanceServices)
                .HasForeignKey(sis => sis.ServiceId);

            // ServiceInstancePart
            modelBuilder.Entity<ServiceInstancePart>()
                .HasKey(sip => new { sip.ServiceInstanceId, sip.PartId });

            modelBuilder.Entity<ServiceInstancePart>()
                .HasOne(sis => sis.ServiceInstance)
                .WithMany(si => si.ServiceInstanceParts)
                .HasForeignKey(sis => sis.ServiceInstanceId);

            modelBuilder.Entity<ServiceInstancePart>()
                .HasOne(sis => sis.Part)
                .WithMany(s => s.ServiceInstanceParts)
                .HasForeignKey(sis => sis.PartId);

            // Mechanic
            modelBuilder.Entity<Mechanic>(e =>
            {
                e.Property(m => m.FirstName)
                .IsRequired();

                e.Property(m => m.LastName)
                .IsRequired();
            });

            // Service
            modelBuilder.Entity<Service>(e =>
            {
                e.Property(s => s.Name)
                .IsRequired();

                e.Property(p => p.Price)
                .IsRequired();
            });

            // Part
            modelBuilder.Entity<Part>(e =>
            {
                e.Property(p => p.Name)
                .IsRequired();

                e.Property(p => p.Price)
                .IsRequired();

                e.Property(p => p.UnitPrice)
                .IsRequired();

                e.Property(p => p.Quantity)
                .IsRequired();
            });
        }
    }
}
