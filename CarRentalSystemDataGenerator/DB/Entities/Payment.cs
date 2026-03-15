using CarRentalSystemDataGenerator.DB.Enums;

namespace CarRentalSystemDataGenerator.DB.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int RentalID { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public PaymentMethod Method { get; set; }

        public Rental Rental { get; set; } = null!;
    }
}
