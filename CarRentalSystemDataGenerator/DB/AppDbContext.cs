using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CarRentalSystemDataGenerator.DB.Entities;
using CarRentalSystemDataGenerator.DB.Enums;

namespace CarRentalSystemDataGenerator.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Model> Models { get; set; } = null!;
        public DbSet<Office> Offices { get; set; } = null!;
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Rental> Rentals { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Maintenance> Maintenances { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure keys and relationships where explicit types/names differ
            modelBuilder.Entity<Address>().HasKey(a => a.AddressID);
            modelBuilder.Entity<Brand>().HasKey(b => b.BrandID);
            modelBuilder.Entity<Model>().HasKey(m => m.ModelID);
            modelBuilder.Entity<Office>().HasKey(o => o.OfficeID);
            modelBuilder.Entity<Car>().HasKey(c => c.CarID);
            modelBuilder.Entity<Customer>().HasKey(c => c.CustomerID);
            modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeID);
            modelBuilder.Entity<Rental>().HasKey(r => r.RentalID);
            modelBuilder.Entity<Payment>().HasKey(p => p.PaymentID);
            modelBuilder.Entity<Maintenance>().HasKey(ms => ms.MaintenanceID);

            modelBuilder.Entity<Model>()
                .HasOne(m => m.Brand)
                .WithMany(b => b.Models)
                .HasForeignKey(m => m.BrandID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Office>()
                .HasOne(o => o.Address)
                .WithMany(a => a.Offices)
                .HasForeignKey(o => o.AddressID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Model)
                .WithMany(m => m.Cars)
                .HasForeignKey(c => c.ModelID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Office)
                .WithMany(o => o.Cars)
                .HasForeignKey(c => c.OfficeID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Address)
                .WithMany(a => a.Customers)
                .HasForeignKey(c => c.AddressID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Office)
                .WithMany(o => o.Employees)
                .HasForeignKey(e => e.OfficeID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CarID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Employee)
                .WithMany(e => e.Rentals)
                .HasForeignKey(r => r.EmployeeID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Rental)
                .WithMany(r => r.Payments)
                .HasForeignKey(p => p.RentalID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Maintenance>()
                .HasOne(m => m.Car)
                .WithMany(c => c.Maintenances)
                .HasForeignKey(m => m.CarID)
                .OnDelete(DeleteBehavior.Cascade);

            // Store enums as strings to match PostgreSQL enum usage or use conversions
            modelBuilder.Entity<Employee>()
                .Property(e => e.Position)
                .HasConversion<string>();

            modelBuilder.Entity<Payment>()
                .Property(p => p.Method)
                .HasConversion<string>();

            // Configure column names similar to SQL script
            modelBuilder.Entity<Address>().Property(a => a.AddressID).HasColumnName("AddressID");
            modelBuilder.Entity<Brand>().Property(b => b.BrandID).HasColumnName("BrandID");
            modelBuilder.Entity<Model>().Property(m => m.ModelID).HasColumnName("ModelID");
            modelBuilder.Entity<Office>().Property(o => o.OfficeID).HasColumnName("OfficeID");
            modelBuilder.Entity<Car>().Property(c => c.CarID).HasColumnName("CarID");
            modelBuilder.Entity<Customer>().Property(c => c.CustomerID).HasColumnName("CustomerID");
            modelBuilder.Entity<Employee>().Property(e => e.EmployeeID).HasColumnName("EmployeeID");
            modelBuilder.Entity<Rental>().Property(r => r.RentalID).HasColumnName("RentalID");
            modelBuilder.Entity<Payment>().Property(p => p.PaymentID).HasColumnName("PaymentID");
            modelBuilder.Entity<Maintenance>().Property(ms => ms.MaintenanceID).HasColumnName("MaintenanceID");
        }
    }
}
