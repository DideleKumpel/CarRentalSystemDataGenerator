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
    internal partial class CustomersCrudeViewModel: ObservableObject
    {
        private readonly IDbServiceInterface<Customer> _customerDbService;

        [ObservableProperty]
        private ObservableCollection<Customer> _customerList;

        [ObservableProperty]
        private Customer _selectedCustomer;

        partial void OnSelectedCustomerChanged(Customer value)
        {
            if (value != null)
            {
                CustomerId = value.CustomerID;
                AddressId = value.AddressID;
                FirstName = value.FirstName;
                LastName = value.LastName;
                Email = value.Email;
                DriverLicenseNum = value.DriverLicenseNum;
            }
            DeleteCustomerCommand.NotifyCanExecuteChanged();
            UpdateCustomerCommand.NotifyCanExecuteChanged();
        }
        [ObservableProperty]
        private int _customerId;
        [ObservableProperty]
        private int _addressId;
        [ObservableProperty]
        private string _firstName;
        [ObservableProperty]
        private string _lastName;
        [ObservableProperty]
        private string _email;
        [ObservableProperty]
        private string _driverLicenseNum;

        public CustomersCrudeViewModel(IDbServiceInterface<Customer> customerDbService)
        {
            _customerDbService = customerDbService;
            LoadCustomers();
        }

        [RelayCommand]
        private void LoadCustomers()
        {
            var customers = _customerDbService.GetAll();
            CustomerList = new ObservableCollection<Customer>(customers);
        }

        [RelayCommand]
        private void AddCustomer()
        {
            var newCustomer = new Customer
            {
                AddressID = AddressId,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                DriverLicenseNum = DriverLicenseNum
            };
            try
            {
                _customerDbService.Add(newCustomer);
                LoadCustomers();
            }
            catch
            {
                MessageBox.Show("Error occured while adding data", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CustomerIsSelected))]
        private void UpdateCustomer()
        {
            if (SelectedCustomer != null)
            {
                var updatedCustomer = new Customer
                {
                    CustomerID= SelectedCustomer.CustomerID,
                    AddressID = AddressId,
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    DriverLicenseNum = DriverLicenseNum
                };
                try
                {
                    _customerDbService.Update(updatedCustomer);
                    LoadCustomers();
                }
                catch
                {
                    MessageBox.Show("Error occured while updating data", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        [RelayCommand(CanExecute = nameof(CustomerIsSelected))]
        private void DeleteCustomer()
        {
            if (SelectedCustomer != null)
            {
                try
                {
                    _customerDbService.Delete(SelectedCustomer.CustomerID);
                    LoadCustomers();
                }
                catch
                {
                    MessageBox.Show("Error occured while deleting data", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool CustomerIsSelected()
        {
            return SelectedCustomer != null;
        }
    }
}
