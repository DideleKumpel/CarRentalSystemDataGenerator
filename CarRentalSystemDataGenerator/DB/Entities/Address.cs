

using System.Text.Json.Serialization;

namespace CarRentalSystemDataGenerator.DB.Entities
{
    public class Address
    {
        public int AddressID { get; set; }
        public string City { get; set; } = null!;
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? PostalCode { get; set; }
        [JsonIgnore]
        public System.Collections.Generic.List<Office> Offices { get; set; } = new();
        [JsonIgnore]
        public System.Collections.Generic.List<Customer> Customers { get; set; } = new();
    }
}
