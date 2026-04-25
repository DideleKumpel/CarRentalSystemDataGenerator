using System.Text.Json.Serialization;

namespace CarRentalSystemDataGenerator.DB.Entities
{
    public class Model
    {
        public int ModelID { get; set; }
        public int BrandID { get; set; }
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public Brand Brand { get; set; } = null!;
        [JsonIgnore]
        public System.Collections.Generic.List<Car> Cars { get; set; } = new();
    }
}
