using CarRentalSystemDataGenerator.DB.Entities;
using CarRentalSystemDataGenerator.Services.DbServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace CarRentalSystemDataGenerator.ViewModel.CrudeViewModels
{
    internal partial class MaintencesCrudeViewModel: ObservableObject
    {
        private readonly IDbServiceInterface<Maintenance> _dbService;

        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private int _carId;
        [ObservableProperty]
        private string _description;
        [ObservableProperty]
        private DateOnly _maintenceDate;
        [ObservableProperty]
        private decimal _cost;

        [ObservableProperty]
        private ObservableCollection<Maintenance> _list;
        [ObservableProperty]
        private Maintenance _selected;

        partial void OnSelectedChanged(Maintenance value)
        {
            if (value != null)
            {
                Id = value.MaintenanceID;
                CarId = value.CarID;
                Description = value.Description;
                MaintenceDate = value.MaintenanceDate;
                Cost = value.Cost;
                RemoveCommand.NotifyCanExecuteChanged();
                UpdateCommand.NotifyCanExecuteChanged();
            }
        }

        public MaintencesCrudeViewModel(IDbServiceInterface<Maintenance> dbService)
        {
            _dbService = dbService;
            Load();
        }

        [RelayCommand]
        private void Load()
        {
            List<Maintenance> list = _dbService.GetAll();
            List = new ObservableCollection<Maintenance>(list);
        }

        [RelayCommand]
        public void Add()
        {
            Maintenance toAdd = new Maintenance
            {
                CarID = CarId,
                Description = Description,
                MaintenanceDate = MaintenceDate,
                Cost = Cost,
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
                var updated = new Maintenance
                {
                    MaintenanceID = Selected.MaintenanceID,
                    CarID = CarId,
                    Description = Description,
                    MaintenanceDate = MaintenceDate,
                    Cost = Cost,
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
