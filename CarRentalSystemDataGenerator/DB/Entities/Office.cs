namespace CarRentalSystemDataGenerator.DB.Entities
{
    public class Office
    {
        public int OfficeID { get; set; }
        public int AddressID { get; set; }
        public string OfficeName { get; set; } = null!;

        public Address Address { get; set; } = null!;
        public System.Collections.Generic.List<Car> Cars { get; set; } = new();
        public System.Collections.Generic.List<Employee> Employees { get; set; } = new();
    }
}
