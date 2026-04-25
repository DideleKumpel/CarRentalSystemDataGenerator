using CarRentalSystemDataGenerator.Models;
using CarRentalSystemDataGenerator.Services;
using CarRentalSystemDataGenerator.Services.DbExportService;
using CarRentalSystemDataGenerator.Services.ImportService;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace CarRentalSystemDataGenerator.ViewModel
{
    internal partial class ExportViewModel : ObservableObject
    {
        private readonly ImportServiceBase<DatabaseSnapshot> _importService;
        private readonly ExportServiceBase<DatabaseSnapshot> _exportService;
        private readonly DatabaseFacade _databaseFacade;

        [ObservableProperty]
        private string _fileExport;

        [ObservableProperty]
        private string _fileImport;



        public ExportViewModel(ImportServiceBase<DatabaseSnapshot> importService, ExportServiceBase<DatabaseSnapshot> exportService, DatabaseFacade databaseFacade)
        {
            _importService = importService;
            _exportService = exportService;
            _databaseFacade = databaseFacade;
        }

        public ExportViewModel()
        {
            _importService = new ImportServiceBase<DatabaseSnapshot>();
            _exportService = new ExportServiceBase<DatabaseSnapshot>();
            _databaseFacade = new DatabaseFacade();
        }

        [RelayCommand]
        private void CreateFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Choose JSON file",
                Filter = "Pliki JSON (*.json)|*.json",
                InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop),
            };

            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                FileExport = saveFileDialog.FileName;
            }
            else
            {
                FileExport = "";
            }
        }

        [RelayCommand]
        private void ChooseFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Choose JSON file",
                Filter = "Pliki JSON (*.json)|*.json",
                InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop),
                CheckFileExists = true,
                CheckPathExists = true,
            };

            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                FileImport = openFileDialog.FileName;
            }
            else
            {
                FileImport = "";
            }
            FileExport = "";
        }

        private DatabaseSnapshot GetDatabaseSnapshot()
        {
            var databaseSnapshot = new DatabaseSnapshot
            {
                Brands = _databaseFacade.BrandDbService.GetAll(),
                Models = _databaseFacade.ModelDbService.GetAll(),
                Addresses = _databaseFacade.AddressDbService.GetAll(),
                Offices = _databaseFacade.OfficeDbService.GetAll(),
                Employees = _databaseFacade.EmployeeDbService.GetAll(),
                Cars = _databaseFacade.CarDbService.GetAll(),
                Customers = _databaseFacade.CustomerDbService.GetAll(),
                Rentals = _databaseFacade.RentalDbService.GetAll(),
                Payments = _databaseFacade.PaymentDbService.GetAll(),
                Maintenances = _databaseFacade.MaintenanceDbService.GetAll()
            };
            return databaseSnapshot;
        }

        [RelayCommand]
        private async Task ExportToJsonAsync()
        {
            List<DatabaseSnapshot> databaseSnapshot = new List<DatabaseSnapshot> { GetDatabaseSnapshot() };
            try
            {
                await _exportService.ExportToJSONAsync(databaseSnapshot, FileExport);
                System.Windows.MessageBox.Show("Export to JSON completed successfully.");
            }
            catch
            {
                System.Windows.MessageBox.Show("An error occurred while exporting to JSON.");
            }
        }

        [RelayCommand]
        private async Task ImportFromJsonAsync()
        {
            try
            {
                List<DatabaseSnapshot> importedData = await _importService.ImportFromJsonAsync(FileImport);
                if (importedData.Count > 0)
                {
                    DatabaseSnapshot snapshot = importedData[0];
                    // Save the imported data to the database
                    _databaseFacade.AddressDbService.AddMany(snapshot.Addresses, false);
                    _databaseFacade.BrandDbService.AddMany(snapshot.Brands, false);
                    _databaseFacade.ModelDbService.AddMany(snapshot.Models, false);
                    _databaseFacade.OfficeDbService.AddMany(snapshot.Offices, false);
                    _databaseFacade.CustomerDbService.AddMany(snapshot.Customers, false);
                    _databaseFacade.EmployeeDbService.AddMany(snapshot.Employees, false);
                    _databaseFacade.CarDbService.AddMany(snapshot.Cars, false);
                    _databaseFacade.MaintenanceDbService.AddMany(snapshot.Maintenances, false);
                    _databaseFacade.RentalDbService.AddMany(snapshot.Rentals, false);
                    _databaseFacade.PaymentDbService.AddMany(snapshot.Payments, false);
                    
                    System.Windows.MessageBox.Show("Import from JSON completed successfully.");
                }
                else
                {
                    System.Windows.MessageBox.Show("No data found in the JSON file.");
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("An error occurred while importing from JSON.");
            }
        }

        [RelayCommand]
        private void BackToMainWindow()
        {
            var mainMenu = ((App)Application.Current).ServiceProvider.GetService<MainMenuViewModel>();
            App.Current.MainWindow.Content = mainMenu;
        }
    }
}
