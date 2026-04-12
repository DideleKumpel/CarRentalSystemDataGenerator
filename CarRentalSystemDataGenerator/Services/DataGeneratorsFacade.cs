using CarRentalSystemDataGenerator.Services.DataGeneratorService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalSystemDataGenerator.Services
{
    internal class DataGeneratorsFacade
    {
        DatabaseFacade databaseFacade;

        public AddressDataGeneratorService AddressDataGenerator;
        public BrandDataGeneratorService BrandDataGenerator;
        public CarDataGeneratorService CarDataGenerator;
        public CustomerDataGeneratorService CustomerDataGenerator;
        public EmployeeDataGeneratorService EmployeeDataGenerator;
        public MaintenanceDataGeneratorService MaintenanceDataGenerator;
        public ModelDataGeneratorService ModelDataGenerator;
        public OfficeDataGeneratorService OfficeDataGenerator;
        public PaymentDataGeneratorService PaymentDataGenerator;
        public RentalDataGeneratorService RentalDataGenerator;

        public DataGeneratorsFacade(DatabaseFacade databaseFacade)
        {
            this.databaseFacade = databaseFacade;
            AddressDataGenerator = new AddressDataGeneratorService();
            BrandDataGenerator = new BrandDataGeneratorService();
            CarDataGenerator = new CarDataGeneratorService();
            CustomerDataGenerator = new CustomerDataGeneratorService();
            EmployeeDataGenerator = new EmployeeDataGeneratorService();
            MaintenanceDataGenerator = new MaintenanceDataGeneratorService();
            ModelDataGenerator = new ModelDataGeneratorService();
            OfficeDataGenerator = new OfficeDataGeneratorService();
            PaymentDataGenerator = new PaymentDataGeneratorService();
            RentalDataGenerator = new RentalDataGeneratorService();
        }
    }
}
