namespace CarRentalSystemDataGenerator.DB.Entities
{
    public class Brand
    {
        public int BrandID { get; set; }
        public string Name { get; set; } = null!;
        public System.Collections.Generic.List<Model> Models { get; set; } = new();
    }
}
