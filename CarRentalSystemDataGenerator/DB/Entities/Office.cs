using System.Text.Json.Serialization;

namespace CarRentalSystemDataGenerator.DB.Entities
{
    public class Office
    {
        public int OfficeID { get; set; }
        public int AddressID { get; set; }
        public string OfficeName { get; set; } = null!;
        [JsonIgnore]
        public Address Address { get; set; } = null!;
        [JsonIgnore]
        public System.Collections.Generic.List<Car> Cars { get; set; } = new();
        [JsonIgnore]
        public System.Collections.Generic.List<Employee> Employees { get; set; } = new();
    }
}
