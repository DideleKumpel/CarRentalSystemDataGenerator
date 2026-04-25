using System.Text.Json.Serialization;

namespace CarRentalSystemDataGenerator.DB.Entities
{
    public class Brand
    {
        public int BrandID { get; set; }
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public System.Collections.Generic.List<Model> Models { get; set; } = new();
    }
}
