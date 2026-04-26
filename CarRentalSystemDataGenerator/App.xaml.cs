using CarRentalSystemDataGenerator.DB;
using CarRentalSystemDataGenerator.DB.Entities;
using CarRentalSystemDataGenerator.Services;
using CarRentalSystemDataGenerator.Services.DataGeneratorService;
using CarRentalSystemDataGenerator.Services.DbExportService;
using CarRentalSystemDataGenerator.Services.DbServices;
using CarRentalSystemDataGenerator.Services.ImportService;
using CarRentalSystemDataGenerator.ViewModel;
using CarRentalSystemDataGenerator.ViewModels.CrudeViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text.Json;
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

            var baseDir = Directory.GetCurrentDirectory();
            baseDir = baseDir.Replace("\\bin\\Debug\\net10.0-windows", ""); // Adjust for typical build output path

            // Try common locations for appsettings.json. EF tools may run with a different working directory,
            // so search the current directory and its subdirectories for the file.
            var path = Path.Combine(baseDir, "appsettings.json");
            if (!File.Exists(path))
            {
                path = Directory.EnumerateFiles(baseDir, "appsettings.json", SearchOption.AllDirectories).FirstOrDefault();
            }

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                throw new FileNotFoundException($"Configuration file not found: appsettings.json (searched under {baseDir})");

            var json = File.ReadAllText(path);
            using var doc = JsonDocument.Parse(json);
            string? conn = null;
            if (doc.RootElement.TryGetProperty("ConnectionStrings", out var cs)
                && cs.TryGetProperty("DefaultConnection", out var dc))
            {
                conn = dc.GetString();
            }

            if (string.IsNullOrEmpty(conn))
                throw new InvalidOperationException("DefaultConnection not found in appsettings.json");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(conn);
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
            services.AddTransient<CrudeViewModel>();

            services.AddTransient<CustomersCrudeViewModel>();


            ServiceProvider = services.BuildServiceProvider();
        }
    }

}
