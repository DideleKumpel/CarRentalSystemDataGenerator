using CarRentalSystemDataGenerator.DB.Entities;

namespace CarRentalSystemDataGenerator.Models
{
    internal class DatabaseSnapshot
    {
        public List<Brand> Brands { get; set; } = new();
        public List<DB.Entities.Model> Models { get; set; } = new();
        public List<Address> Addresses { get; set; } = new();
        public List<Office> Offices { get; set; } = new();
        public List<Employee> Employees { get; set; } = new();
        public List<Car> Cars { get; set; } = new();
        public List<Customer> Customers { get; set; } = new();
        public List<Rental> Rentals { get; set; } = new();
        public List<Payment> Payments { get; set; } = new();
        public List<Maintenance> Maintenances { get; set; } = new();
    }
}
