using CarRentalSystemDataGenerator.DB.Entities;
using CarRentalSystemDataGenerator.Services.DbServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace CarRentalSystemDataGenerator.ViewModel.CrudeViewModels
{
    partial class CarsCrudeViewModel: ObservableObject
    {
        private readonly IDbServiceInterface<Car> _dbService;

        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private int _modelId;
        [ObservableProperty]
        private int _officeId;
        [ObservableProperty]
        private string _licensePlate;
        [ObservableProperty]
        private int _year;
        [ObservableProperty]
        private Decimal _dailyRate;

        [ObservableProperty]
        private ObservableCollection<Car> _list;
        [ObservableProperty]
        private Car _selected;

        partial void OnSelectedChanged(Car value)
        {
            if (_selected != value)
            {
                Id = Selected.CarID;
                ModelId = Selected.ModelID;
                OfficeId = Selected.OfficeID;
                LicensePlate = Selected.LicensePlate;
                Year = Selected.Year;
                DailyRate = Selected.DailyRate;
            }
        }

        public CarsCrudeViewModel(IDbServiceInterface<Car> dbService)
        {
            _dbService = dbService;
        }

        [RelayCommand]
        private void Load()
        {
            List<Car> list = _dbService.GetAll();
            List = new ObservableCollection<Car>(list);
        }

        [RelayCommand]
        public void Add()
        {
            if (string.IsNullOrEmpty(LicensePlate))
            {
                MessageBox.Show("Fill forms", " Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Car toAdd = new Car
            {
                OfficeID = OfficeId,
                ModelID = ModelId,
                LicensePlate = LicensePlate,
                Year = Year,
                DailyRate = DailyRate
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
                var updated = new Car
                {
                    ModelID = ModelId,
                    OfficeID = OfficeId,
                    LicensePlate = LicensePlate,
                    Year = Year,
                    DailyRate= DailyRate
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
