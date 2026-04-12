using CarRentalSystemDataGenerator.DB;
using CarRentalSystemDataGenerator.Services.DbServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalSystemDataGenerator.Services
{
    internal class DatabaseFacade
    {
        private AppDbContext appDbContext;

        public AddressDbService AddressDbService;
        public BrandDbService BrandDbService;
        public CarDbService CarDbService;
        public CustomerDbService CustomerDbService;
        public EmployeeDbService EmployeeDbService;
        public MaintenanceDbService MaintenanceDbService;
        public ModelDbService ModelDbService;
        public OfficeDbService OfficeDbService;
        public PaymentDbService PaymentDbService;
        public RentalDbService RentalDbService;

        public DatabaseFacade()
        {
            appDbContext = DbContextFactory.CreateFromAppSettings();

            AddressDbService = new AddressDbService(appDbContext);
            BrandDbService = new BrandDbService(appDbContext);
            CarDbService = new CarDbService(appDbContext);
            CustomerDbService = new CustomerDbService(appDbContext);
            EmployeeDbService = new EmployeeDbService(appDbContext);
            MaintenanceDbService = new MaintenanceDbService(appDbContext);
            ModelDbService = new ModelDbService(appDbContext);
            OfficeDbService = new OfficeDbService(appDbContext);
            PaymentDbService = new PaymentDbService(appDbContext);
            RentalDbService = new RentalDbService(appDbContext);
        }

        public DatabaseFacade(AddressDbService addressDbService, BrandDbService brandDbService, CarDbService carDbService, CustomerDbService customerDbService, EmployeeDbService employeeDbService, MaintenanceDbService maintenanceDbService, ModelDbService modelDbService, OfficeDbService officeDbService, PaymentDbService paymentDbService, RentalDbService rentalDbService)
        {
            appDbContext = DbContextFactory.CreateFromAppSettings();

            this.AddressDbService = addressDbService;
            this.BrandDbService = brandDbService;
            this.CarDbService = carDbService;
            this.CustomerDbService = customerDbService;
            this.EmployeeDbService = employeeDbService;
            this.MaintenanceDbService = maintenanceDbService;
            this.ModelDbService = modelDbService;
            this.OfficeDbService = officeDbService;
            this.PaymentDbService = paymentDbService;
            this.RentalDbService = rentalDbService;
        }
    }
}
