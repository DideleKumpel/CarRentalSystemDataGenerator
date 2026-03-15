namespace CarRentalSystemDataGenerator.DB.Entities
{
    public class Model
    {
        public int ModelID { get; set; }
        public int BrandID { get; set; }
        public string Name { get; set; } = null!;

        public Brand Brand { get; set; } = null!;
        public System.Collections.Generic.List<Car> Cars { get; set; } = new();
    }
}
