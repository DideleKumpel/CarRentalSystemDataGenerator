using CarRentalSystemDataGenerator.DB.Entities;
using CarRentalSystemDataGenerator.Services.DbServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace CarRentalSystemDataGenerator.ViewModel.CrudeViewModels
{
    partial class RentalsCrudeViewModel: ObservableObject
    {
        private readonly IDbServiceInterface<Rental> _dbService;

        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private int _carId;
        [ObservableProperty]
        private int _customerId;
        [ObservableProperty]
        private int? _employerId;
        [ObservableProperty]
        private DateTime _rentalDate;
        [ObservableProperty]
        private DateTime? _returnDate;
        [ObservableProperty]
        private decimal _totalCost;

        [ObservableProperty]
        private ObservableCollection<Rental> _list;
        [ObservableProperty]
        private Rental _selected;

        partial void OnSelectedChanged(Rental value)
        {
            if (value != null)
            {
                Id = value.RentalID;
                CarId = value.CarID;
                CustomerId = value.CustomerID;
                EmployerId = value.EmployeeID;    
                RentalDate = value.RentalDate;
                ReturnDate = value.ReturnDate;
                TotalCost = value.TotalCost;
            }
        }

        public RentalsCrudeViewModel(IDbServiceInterface<Rental> dbService)
        {
            _dbService = dbService;
            Load();
        }

        [RelayCommand]
        private void Load()
        {
            List<Rental> list = _dbService.GetAll();
            List = new ObservableCollection<Rental>(list);
        }

        [RelayCommand]
        public void Add()
        {
            Rental toAdd = new Rental
            {
                CarID = CarId,
                CustomerID = CustomerId,
                EmployeeID = EmployerId,
                RentalDate = RentalDate,
                ReturnDate = ReturnDate,
                TotalCost = TotalCost,
            };
            try
            {
                _dbService.Add(toAdd);
                MessageBox.Show("Added", " Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
                Load();
            }
            catch
            {
                MessageBox.Show("Error ocurred whlie adding", " Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(IsSelected))]
        public void Remove()
        {
            if (Selected != null)
            {
                try
                {
                    _dbService.Delete(Selected);
                    MessageBox.Show("Deleted");
                    Load();
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
        }

        [RelayCommand(CanExecute = nameof(IsSelected))]
        private void Update()
        {
            if (Selected != null)
            {
                var updated = new Rental
                {
                    RentalID = Selected.RentalID,
                    CarID = CarId,
                    CustomerID = CustomerId,
                    EmployeeID = EmployerId,
                    RentalDate = RentalDate,
                    ReturnDate = ReturnDate,
                    TotalCost = TotalCost,
                };
                try
                {
                    _dbService.Update(updated);
                    Load();
                    MessageBox.Show("Added", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch
                {
                    MessageBox.Show("Error", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool IsSelected()
        {
            return Selected != null;
        }
    }
}
