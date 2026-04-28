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
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace CarRentalSystemDataGenerator.ViewModel.CrudeViewModels
{
    internal partial class PaymentsCrudeViewModel: ObservableObject
    {
        private readonly IDbServiceInterface<Payment> _dbService;

        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        public int _rentalID;
        [ObservableProperty]
        public decimal _amount;
        [ObservableProperty]
        public System.DateTime _paymentDate;
        [ObservableProperty]
        public PaymentMethod _method;

        [ObservableProperty]
        private ObservableCollection<Payment> _list;
        [ObservableProperty]
        private Payment _selected;

        partial void OnSelectedChanged(Payment value)
        {
            if (value != null)
            {
                Id = value.PaymentID;
                RentalID = value.RentalID;
                Amount = value.Amount;
                PaymentDate = value.PaymentDate;
                Method = value.Method;
            }
        }

        public PaymentsCrudeViewModel(IDbServiceInterface<Payment> dbService)
        {
            _dbService = dbService;
            Load();
        }

        [RelayCommand]
        private void Load()
        {
            List<Payment> list = _dbService.GetAll();
            List = new ObservableCollection<Payment>(list);
        }

        [RelayCommand]
        public void Add()
        {
            Payment toAdd = new Payment()
            {
                RentalID = RentalID,
                Amount = Amount,
                PaymentDate = PaymentDate,
                Method = Method
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
                var updated = new Payment
                {
                    PaymentID = Selected.PaymentID,
                    RentalID = RentalID,
                    Amount = Amount,
                    PaymentDate = PaymentDate,
                    Method = Method
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
