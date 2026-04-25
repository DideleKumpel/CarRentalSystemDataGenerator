using System.Text.Json.Serialization;

namespace CarRentalSystemDataGenerator.DB.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public int AddressID { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string DriverLicenseNum { get; set; } = null!;
        [JsonIgnore]
        public Address Address { get; set; } = null!;
        [JsonIgnore]
        public System.Collections.Generic.List<Rental> Rentals { get; set; } = new();
    }
}
