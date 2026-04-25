using CarRentalSystemDataGenerator.DB.Enums;
using System.Text.Json.Serialization;

namespace CarRentalSystemDataGenerator.DB.Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public int OfficeID { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public EmployeePosition Position { get; set; }
        [JsonIgnore]
        public Office Office { get; set; } = null!;
        [JsonIgnore]
        public System.Collections.Generic.List<Rental> Rentals { get; set; } = new();
    }
}
