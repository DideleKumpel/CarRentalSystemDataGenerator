using CarRentalSystemDataGenerator.DB.Entities;
using CarRentalSystemDataGenerator.DB.Enums;
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
    internal partial class EmployeesCrudeViewModel: ObservableObject
    {
        private readonly IDbServiceInterface<Employee> _dbService;

        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private int _officeId;
        [ObservableProperty]
        private string _firstName;
        [ObservableProperty]
        private string _lastName;
        [ObservableProperty]
        private EmployeePosition _position;

        [ObservableProperty]
        private ObservableCollection<Employee> _employees;
        [ObservableProperty]
        private Employee _selectedEmployee;

        partial void OnSelectedEmployeeChanged(Employee value)
        {
            if (_selectedEmployee != null) {
                Id = value.EmployeeID;
                OfficeId = value.OfficeID;
                FirstName = value.FirstName;
                LastName = value.LastName;
                Position = value.Position;
            }
            RemoveCommand.NotifyCanExecuteChanged();
            UpdateCommand.NotifyCanExecuteChanged();
        }


        public EmployeesCrudeViewModel(IDbServiceInterface<Employee> dbService) {
            _dbService = dbService;
            Load();
        }
        [RelayCommand]
        public void Load() {
            List<Employee> list = _dbService.GetAll();
            Employees = new ObservableCollection<Employee>(list);
        }

        [RelayCommand]
        public void Add(Employee employee) {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
            {
                MessageBox.Show("Fill forms", " Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Employee employeeToAdd = new Employee
            {
                OfficeID = OfficeId,
                FirstName = FirstName,
                LastName = LastName,
                Position = Position,
            };
            try
            {
                _dbService.Add(employeeToAdd);
                MessageBox.Show("Added", " Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
                Load();
            }
            catch
            {
                MessageBox.Show("Error ocurred whlie adding", " Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(IsSelected))]
        public void Remove(Employee employee) {
            if (SelectedEmployee != null)
            {
                try
                {
                    _dbService.Delete(SelectedEmployee);
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
            if (SelectedEmployee != null)
            {
                var updatedCustomer = new Employee
                {
                    EmployeeID = SelectedEmployee.EmployeeID,
                    OfficeID = OfficeId,
                    FirstName = FirstName,
                    LastName = LastName,
                    Position = Position
                };
                try
                {
                    _dbService.Update(updatedCustomer);
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
            return SelectedEmployee != null;
        }
    }
}
