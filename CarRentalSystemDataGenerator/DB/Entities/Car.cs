namespace CarRentalSystemDataGenerator.DB.Entities
{
    public class Car
    {
        public int CarID { get; set; }
        public int ModelID { get; set; }
        public int OfficeID { get; set; }
        public string LicensePlate { get; set; } = null!;
        public int Year { get; set; }
        public decimal DailyRate { get; set; }

        public Model Model { get; set; } = null!;
        public Office Office { get; set; } = null!;
        public System.Collections.Generic.List<Rental> Rentals { get; set; } = new();
        public System.Collections.Generic.List<Maintenance> Maintenances { get; set; } = new();
    }
}
