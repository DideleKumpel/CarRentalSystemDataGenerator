namespace CarRentalSystemDataGenerator.DB.Entities
{
    public class Rental
    {
        public int RentalID { get; set; }
        public int CarID { get; set; }
        public int CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public System.DateTime RentalDate { get; set; }
        public System.DateTime? ReturnDate { get; set; }
        public decimal TotalCost { get; set; }

        public Car Car { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        public Employee? Employee { get; set; }
        public System.Collections.Generic.List<Payment> Payments { get; set; } = new();
    }
}
