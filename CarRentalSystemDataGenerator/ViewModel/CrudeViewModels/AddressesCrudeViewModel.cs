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
    internal partial class AddressesCrudeViewModel: ObservableObject
    {
        private readonly IDbServiceInterface<Address> _dbService;

        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private string _city;
        [ObservableProperty]
        private string _street;
        [ObservableProperty]
        private string _houseNum;
        [ObservableProperty]
        private string _postalCode;

        [ObservableProperty]
        ObservableCollection<Address> _addressesList;
        [ObservableProperty]
        private Address _selectedAddress;

        partial void OnSelectedAddressChanged(Address value)
        {
            if (_selectedAddress != null) {
                Id = value.AddressID;
                City = value.City;
                Street = value.Street;
                HouseNum = value.HouseNumber;
                PostalCode = value.PostalCode;
            }
            DeleteAddressCommand.NotifyCanExecuteChanged();
            UpdateAddressCommand.NotifyCanExecuteChanged();
        }

        public AddressesCrudeViewModel( IDbServiceInterface<Address> dbService)
        {
            _dbService = dbService;
            LoadAddresses();
        }

        [RelayCommand]
        public void LoadAddresses()
        {
            List<Address> addresesFromDb = _dbService.GetAll();
            AddressesList = new ObservableCollection<Address>(addresesFromDb);
        }

        [RelayCommand]
        private void AddAddress()
        {
            if(string.IsNullOrEmpty(City) || string.IsNullOrEmpty(Street) || string.IsNullOrWhiteSpace(HouseNum) || string.IsNullOrWhiteSpace(PostalCode))
            {
                MessageBox.Show("Fill forms", " Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Address addresToAdd = new Address
            {
                City = City,
                Street = Street,
                HouseNumber = HouseNum,
                PostalCode = PostalCode
            };
            try
            {
                _dbService.Add(addresToAdd);
                MessageBox.Show("Address added", " Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
                LoadAddresses();
            }
            catch
            {
                MessageBox.Show("Error ocurred whlie adding address", " Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(AddressIsSelected))]
        private void DeleteAddress()
        {
            if (SelectedAddress != null)
            {
                try
                {
                    _dbService.Delete(SelectedAddress);
                    MessageBox.Show("Addres deleted", " Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch
                {
                    MessageBox.Show("Error ocurred whlie deleting address", " Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                LoadAddresses();
            }
            else
            {
                MessageBox.Show("Error ocurred whlie deleting address", " Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(AddressIsSelected))]
        private void UpdateAddress()
        {
            if (SelectedAddress != null)
            {
                try
                {
                    Address updateAddres = new Address()
                    {
                        AddressID = SelectedAddress.AddressID,
                        HouseNumber = HouseNum,
                        City = City,
                        Street = Street,
                        PostalCode = PostalCode,
                    };
                    _dbService.Update(updateAddres);
                    MessageBox.Show("Addres deleted", " Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch
                {
                    MessageBox.Show("Error ocurred whlie deleting address", " Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                LoadAddresses();
            }
            else
            {
                MessageBox.Show("Error ocurred whlie adding address", " Missing data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool AddressIsSelected() { 
            return SelectedAddress != null; 
        }
    }
}
