using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Smart_Garage.Models.Generators;
using Smart_Garage.Services;
using System.Numerics;

namespace Smart_Garage.Models
{
    public class SGContext : DbContext
    {
        public SGContext(DbContextOptions<SGContext> options): base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleBrand> Brands { get; set; }
        public DbSet<VehicleModel> Models { get; set; }
        public DbSet<Visit> Visits { get; set; }

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

                //e.Property(s => s.TotalPrice)
                //.IsRequired();

                //e.Property(s => s.ServicesTotalPrice)
                //.IsRequired();

                //e.Property(s => s.PartsTotalPrice)
                //.IsRequired();
            });

            // Mechanic
            modelBuilder.Entity<Mechanic>(e =>
            {
                e.Property(m => m.Name)
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

                e.Property(p => p.UnitPrice)
                .IsRequired();

            });

            // PartVisit
            modelBuilder.Entity<PartVisit>()
                .HasKey(pv => new { pv.PartId, pv.VisitId }); // Define composite primary key

            modelBuilder.Entity<PartVisit>()
                .HasOne(pv => pv.Part)
                .WithMany(p => p.PartVisits) // Configure navigation property on Part
                .HasForeignKey(pv => pv.PartId);

            modelBuilder.Entity<PartVisit>()
                .HasOne(pv => pv.Visit)
                .WithMany(v => v.PartVisits) // Configure navigation property on Visit
                .HasForeignKey(pv => pv.VisitId);

            // ServiceVisit
            modelBuilder.Entity<ServiceVisit>()
                .HasKey(sv => new { sv.ServiceId, sv.VisitId }); // Define composite primary key

            modelBuilder.Entity<ServiceVisit>()
                .HasOne(sv => sv.Service)
                .WithMany(s => s.ServiceVisits) // Configure navigation property on Service
                .HasForeignKey(sv => sv.ServiceId);

            modelBuilder.Entity<ServiceVisit>()
                .HasOne(sv => sv.Visit)
                .WithMany(v => v.ServiceVisits) // Configure navigation property on Visit
                .HasForeignKey(sv => sv.VisitId);

            // MechanicVisit
            modelBuilder.Entity<MechanicVisit>()
                .HasKey(mv => new { mv.MechanicId, mv.VisitId }); // Define composite primary key

            modelBuilder.Entity<MechanicVisit>()
                .HasOne(mv => mv.Mechanic)
                .WithMany(m => m.MechanicVisits) // Configure navigation property on Mechanic
                .HasForeignKey(mv => mv.MechanicId);

            modelBuilder.Entity<MechanicVisit>()
                .HasOne(mv => mv.Visit)
                .WithMany(v => v.MechanicVisits) // Configure navigation property on Visit
                .HasForeignKey(mv => mv.VisitId);



            modelBuilder.Entity<User>().HasData(UserGenerator.CreateUsers());
            modelBuilder.Entity<VehicleBrand>().HasData(BrandGenerator.CreateBrands());
            modelBuilder.Entity<VehicleModel>().HasData(ModelGenerator.CreateModels());
            modelBuilder.Entity<Mechanic>().HasData(MechanicGenerator.CreateMechanics());
            modelBuilder.Entity<Part>().HasData(PartGenerator.CreateParts());
            modelBuilder.Entity<Service>().HasData(ServiceGenerator.CreateServices());
            modelBuilder.Entity<Vehicle>().HasData(VehicleGenerator.CreateVehicles());
            modelBuilder.Entity<Visit>().HasData(VisitGenerator.CreateVisits());
            modelBuilder.Entity<PartVisit>().HasData(PartVisitGenerator.CreateVisitParts());
            modelBuilder.Entity<ServiceVisit>().HasData(ServiceVisitGenerator.CreateServiceVisit());
            modelBuilder.Entity<MechanicVisit>().HasData(MechanicVisitGenerator.CreateMechanicVisit());
        }
    }
}
