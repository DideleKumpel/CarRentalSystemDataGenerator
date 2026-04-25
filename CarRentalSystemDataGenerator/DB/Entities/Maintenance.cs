using System.Text.Json.Serialization;

namespace CarRentalSystemDataGenerator.DB.Entities
{
    public class Maintenance
    {
        public int MaintenanceID { get; set; }
        public int CarID { get; set; }
        public string Description { get; set; } = null!;
        public System.DateOnly MaintenanceDate { get; set; }
        public decimal Cost { get; set; }

        [JsonIgnore]
        public Car Car { get; set; } = null!;
    }
}
