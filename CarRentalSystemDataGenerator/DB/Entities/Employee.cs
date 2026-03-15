using CarRentalSystemDataGenerator.DB.Enums;

namespace CarRentalSystemDataGenerator.DB.Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public int OfficeID { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public EmployeePosition Position { get; set; }

        public Office Office { get; set; } = null!;
        public System.Collections.Generic.List<Rental> Rentals { get; set; } = new();
    }
}
