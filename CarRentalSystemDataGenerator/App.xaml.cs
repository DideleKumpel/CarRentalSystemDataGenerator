using CarRentalSystemDataGenerator.DB;
using CarRentalSystemDataGenerator.DB.Entities;
using CarRentalSystemDataGenerator.Services;
using CarRentalSystemDataGenerator.Services.DataGeneratorService;
using CarRentalSystemDataGenerator.Services.DbExportService;
using CarRentalSystemDataGenerator.Services.DbServices;
using CarRentalSystemDataGenerator.Services.ImportService;
using CarRentalSystemDataGenerator.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CarRentalSystemDataGenerator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public App()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
            {
                string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                options.UseNpgsql(connection);
            });

            services.AddScoped<IDbServiceInterface<Address>, AddressDbService>();
            services.AddScoped<IDbServiceInterface<Brand>, BrandDbService>();
            services.AddScoped<IDbServiceInterface<Car>, CarDbService>();
            services.AddScoped<IDbServiceInterface<Customer>, CustomerDbService>();
            services.AddScoped<IDbServiceInterface<Employee>, EmployeeDbService>();
            services.AddScoped<IDbServiceInterface<Model>, ModelDbService>();
            services.AddScoped<IDbServiceInterface<Maintenance>, MaintenanceDbService>();
            services.AddScoped<IDbServiceInterface<Office>, OfficeDbService>();
            services.AddScoped<IDbServiceInterface<Payment>, PaymentDbService>();
            services.AddScoped<IDbServiceInterface<Rental>, RentalDbService>();

            services.AddScoped<DatabaseFacade>();

            services.AddScoped<IDataGeneratorServiceInterface<Address>, AddressDataGeneratorService>();
            services.AddScoped<IDataGeneratorServiceInterface<Brand>, BrandDataGeneratorService>();
            services.AddScoped<IDataGeneratorServiceInterface<Car>, CarDataGeneratorService>();
            services.AddScoped<IDataGeneratorServiceInterface<Customer>, CustomerDataGeneratorService>();
            services.AddScoped<IDataGeneratorServiceInterface<Employee>, EmployeeDataGeneratorService>();
            services.AddScoped<IDataGeneratorServiceInterface<Model>, ModelDataGeneratorService>();
            services.AddScoped<IDataGeneratorServiceInterface<Maintenance>, MaintenanceDataGeneratorService>();
            services.AddScoped<IDataGeneratorServiceInterface<Office>, OfficeDataGeneratorService>();
            services.AddScoped<IDataGeneratorServiceInterface<Payment>, PaymentDataGeneratorService>();
            services.AddScoped<IDataGeneratorServiceInterface<Rental>, RentalDataGeneratorService>();

            services.AddScoped<DataGeneratorsFacade>();

            services.AddScoped(typeof(IImportService<>), typeof(ImportServiceBase<>));
            services.AddScoped(typeof(IExportServiceInterface<>), typeof(ExportServiceBase<>));
            
            services.AddTransient<MainMenuViewModel>();
            services.AddTransient<ExportViewModel>();
            services.AddTransient<GeneratorViewModel>();


            ServiceProvider = services.BuildServiceProvider();
        }
    }

}
