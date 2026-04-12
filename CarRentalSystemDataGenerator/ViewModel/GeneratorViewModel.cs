using CarRentalSystemDataGenerator.DB.Entities;
using CarRentalSystemDataGenerator.Services;
using CarRentalSystemDataGenerator.Services.DbServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace CarRentalSystemDataGenerator.ViewModel
{
    internal partial class GeneratorViewModel : ObservableObject
    {
        public GeneratorViewModel()
        {
            _dbFacade = new DatabaseFacade();
            _dataGeneratorsFacade = new DataGeneratorsFacade(_dbFacade);
        }

        //Generator flags
        [ObservableProperty]
        private bool _adressesIsChecked;
        [ObservableProperty]
        private bool _brandsIsChecked;
        [ObservableProperty]
        private bool _carsIsChecked;
        [ObservableProperty]
        private bool _customersIsChecked;
        [ObservableProperty]
        private bool _employeesIsChecked;
        [ObservableProperty]
        private bool _maintenancesIsChecked;
        [ObservableProperty]
        private bool _modelsIsChecked;
        [ObservableProperty]
        private bool _officesIsChecked;
        [ObservableProperty]
        private bool _paymentsIsChecked;
        [ObservableProperty]
        private bool _rentalsIsChecked;

        [ObservableProperty]
        private int _numOfRecordToGenreate;

        private DatabaseFacade _dbFacade;
        private DataGeneratorsFacade _dataGeneratorsFacade;

        [RelayCommand]
        private void GenerateData()
        {
            if (NumOfRecordToGenreate <= 0) return;

            List<Address> addresses = new List<Address>();
            addresses.AddRange(_dbFacade.AddressDbService.GetAll());
            if (_adressesIsChecked)
            {
                try
                {
                    List<Address> GeneratedAdesses = GenerateAddres();
                    int numOfErrors = _dbFacade.AddressDbService.AddMany(GeneratedAdesses, false);
                    addresses = _dbFacade.AddressDbService.GetAll();
                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }
            }

            List<Brand> brands = new List<Brand>();
            brands.AddRange(_dbFacade.BrandDbService.GetAll());
            if (_brandsIsChecked)
            {
                try
                {
                    List<Brand> GeneratedBrands = GenerateBrands();
                    int numOfErrors = _dbFacade.BrandDbService.AddMany(GeneratedBrands, false);
                    brands = _dbFacade.BrandDbService.GetAll();
                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }
            }

            List<Model> models = new List<Model>();
            models.AddRange(_dbFacade.ModelDbService.GetAll());
            if (_modelsIsChecked)
            {
                try
                {
                    List<Model> GeneratedModels = GenerateModels(brands);
                    int numOfErrors = _dbFacade.ModelDbService.AddMany(GeneratedModels, false);
                    models = _dbFacade.ModelDbService.GetAll();

                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }
            }

            List<Office> offices = new List<Office>();
            offices.AddRange(_dbFacade.OfficeDbService.GetAll());
            if (_modelsIsChecked)
            {
                try
                {
                    List<Office> GeneratedOffices = GenerateOffices(addresses);
                    int numOfErrors = _dbFacade.OfficeDbService.AddMany(GeneratedOffices, false);
                    offices = _dbFacade.OfficeDbService.GetAll();
                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }
            }

            List<Customer> customers = new List<Customer>();
            customers.AddRange(_dbFacade.CustomerDbService.GetAll());
            if (_adressesIsChecked)
            {
                try
                {
                    List<Customer> GeneratedCustomers = GenerateCustomers(addresses);
                    int numOfErrors = _dbFacade.CustomerDbService.AddMany(GeneratedCustomers, false);
                    customers = _dbFacade.CustomerDbService.GetAll();
                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }
            }

            List<Employee> employees = new List<Employee>();
            employees.AddRange(_dbFacade.EmployeeDbService.GetAll());
            if (_adressesIsChecked)
            {
                try
                {
                    List<Employee> GeneratedEmployees = GenerateEmployees(offices);
                    int numOfErrors = _dbFacade.EmployeeDbService.AddMany(GeneratedEmployees, false);
                    employees = _dbFacade.EmployeeDbService.GetAll();
                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }
            }


            List<Car> cars = new List<Car>();
            cars.AddRange(_dbFacade.CarDbService.GetAll());
            if (_carsIsChecked)
            {
                try
                {
                    List<Car> GeneratedCars = GenerateCars(models, offices);
                    int numOfErrors = _dbFacade.CarDbService.AddMany(GeneratedCars, false);
                    cars = _dbFacade.CarDbService.GetAll();
                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }
            }

            List<Maintenance> maintenances = new List<Maintenance>();
            maintenances.AddRange(_dbFacade.MaintenanceDbService.GetAll());
            if (_maintenancesIsChecked)
            {
                try
                {
                    List<Maintenance> GeneratedMaintenances = GenerateMaintenances(cars);
                    int numOfErrors = _dbFacade.MaintenanceDbService.AddMany(GeneratedMaintenances, false);
                    maintenances = _dbFacade.MaintenanceDbService.GetAll();
                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }
            }

            List<Rental> rentals = new List<Rental>();
            rentals.AddRange(_dbFacade.RentalDbService.GetAll());
            if (_rentalsIsChecked)
            {
                try
                {
                    List<Rental> GeneratedRentals = GenerateRentals(cars, customers, employees);
                    int numOfErrors = _dbFacade.RentalDbService.AddMany(GeneratedRentals, false);
                    rentals = _dbFacade.RentalDbService.GetAll();

                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }
            }

            List<Payment> payments = new List<Payment>();
            payments.AddRange(_dbFacade.PaymentDbService.GetAll());
            if (_paymentsIsChecked)
            {
                try
                {
                    List<Payment> GeneratedPayments = GeneratePayments(rentals);
                    int numOfErrors = _dbFacade.PaymentDbService.AddMany(GeneratedPayments, false);
                    payments = _dbFacade.PaymentDbService.GetAll();
                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }
            }
        }

        private List<Address> GenerateAddres()
        {
            return _dataGeneratorsFacade.AddressDataGenerator.GenerateData(NumOfRecordToGenreate);
        }

        private List<Brand> GenerateBrands()
        {
            return _dataGeneratorsFacade.BrandDataGenerator.GenerateData(NumOfRecordToGenreate);
        }

        private List<Model> GenerateModels(List<Brand> brands)
        {
            if (brands.Count == 0)
                throw new Exception(message: "To generate models you need to have at least one brand in the database or select the option to generate brands.");
            return _dataGeneratorsFacade.ModelDataGenerator.GenerateData(NumOfRecordToGenreate, brands);
        }

        private List<Office> GenerateOffices(List<Address> addresses)
        {
            if (addresses.Count == 0)
                throw new Exception(message: "To generate offices you need to have at least one address in the database or select the option to generate addresses.");
            return _dataGeneratorsFacade.OfficeDataGenerator.GenerateData(NumOfRecordToGenreate, addresses);
        }

        private List<Customer> GenerateCustomers(List<Address> addresses)
        {
            if (addresses.Count == 0)
                throw new Exception(message: "To generate customers you need to have at least one address in the database or select the option to generate addresses.");
            return _dataGeneratorsFacade.CustomerDataGenerator.GenerateData(NumOfRecordToGenreate, addresses);
        }

        private List<Employee> GenerateEmployees(List<Office> offices)
        {
            if (offices.Count == 0)
                throw new Exception(message: "To generate employees you need to have at least one office in the database or select the option to generate offices.");
            return _dataGeneratorsFacade.EmployeeDataGenerator.GenerateData(NumOfRecordToGenreate, offices);
        }

        private List<Car> GenerateCars(List<Model> models, List<Office> offices)
        {
            if (models.Count == 0)
                throw new Exception(message: "To generate cars you need to have at least one model in the database or select the option to generate models.");
            if (offices.Count == 0)
                throw new Exception(message: "To generate cars you need to have at least one office in the database or select the option to generate offices.");
            return _dataGeneratorsFacade.CarDataGenerator.GenerateData(NumOfRecordToGenreate, models, offices);
        }

        private List<Maintenance> GenerateMaintenances(List<Car> cars)
        {
            if (cars.Count == 0)
                throw new Exception(message: "To generate maintenances you need to have at least one car in the database or select the option to generate cars.");
            return _dataGeneratorsFacade.MaintenanceDataGenerator.GenerateData(NumOfRecordToGenreate, cars);
        }

        private List<Payment> GeneratePayments(List<Rental> rentals)
        {
            if (rentals.Count == 0)
                throw new Exception(message: "To generate payments you need to have at least one rental in the database or select the option to generate rentals.");
            return _dataGeneratorsFacade.PaymentDataGenerator.GenerateData(NumOfRecordToGenreate, rentals);
        }

        private List<Rental> GenerateRentals(List<Car> cars, List<Customer> customers, List<Employee> employees)
        {
            if (cars.Count == 0)
                throw new Exception(message: "To generate rentals you need to have at least one car in the database or select the option to generate cars.");
            if (customers.Count == 0)
                throw new Exception(message: "To generate rentals you need to have at least one customer in the database or select the option to generate customers.");
            if (employees.Count == 0)
                throw new Exception(message: "To generate rentals you need to have at least one employee in the database or select the option to generate employees.");
            return _dataGeneratorsFacade.RentalDataGenerator.GenerateData(NumOfRecordToGenreate, cars, customers, employees);
        }
    }
}
