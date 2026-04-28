using CarRentalSystemDataGenerator.View;
using CarRentalSystemDataGenerator.ViewModel.CrudeViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CarRentalSystemDataGenerator.ViewModel
{
    internal partial class CrudeViewModel : ObservableObject
    {
        [ObservableProperty]
        private object _currentViewModel;

        private CustomersCrudeViewModel _customersCrudeViewModel;
        private AddressesCrudeViewModel _addressesCrudeViewModel;
        private BrandsCrudeViewModel _brandsCrudeViewModel;
        private EmployeesCrudeViewModel _employeesCrudeViewModel;
        private CarsCrudeViewModel _carsCrudeViewModel;
        private RentalsCrudeViewModel _rentalsCrudeViewModel;
        private PaymentsCrudeViewModel _paymentsCrudeViewModel;
        private MaintencesCrudeViewModel _maintencesCrudeViewModel;

        public CrudeViewModel()
        {
            _addressesCrudeViewModel = ((App)Application.Current).ServiceProvider.GetService<AddressesCrudeViewModel>();
            _customersCrudeViewModel = ((App)Application.Current).ServiceProvider.GetService<CustomersCrudeViewModel>();
            _brandsCrudeViewModel = ((App)Application.Current).ServiceProvider.GetService<BrandsCrudeViewModel>();
            _employeesCrudeViewModel = ((App)Application.Current).ServiceProvider.GetService<EmployeesCrudeViewModel>();
            _carsCrudeViewModel = ((App)Application.Current).ServiceProvider.GetService<CarsCrudeViewModel>();
            _rentalsCrudeViewModel = ((App)Application.Current).ServiceProvider.GetService<RentalsCrudeViewModel>();
            _paymentsCrudeViewModel = ((App)Application.Current).ServiceProvider.GetService<PaymentsCrudeViewModel>();
            _maintencesCrudeViewModel = ((App)Application.Current).ServiceProvider.GetService<MaintencesCrudeViewModel>();
            CurrentViewModel = _customersCrudeViewModel;
        }

        [RelayCommand]
        private void BackToMainWindow()
        {
            App.Current.MainWindow.Content = new MainMenuView();
        }

        [RelayCommand]
        private void SwitchToAddresses()
        {
            CurrentViewModel = _addressesCrudeViewModel;
        }

        [RelayCommand]
        private void SwitchToCustomers()
        {
            CurrentViewModel = _customersCrudeViewModel;
        }

        [RelayCommand]
        private void SwitchToBrands() {
            CurrentViewModel = _brandsCrudeViewModel;
        }

        [RelayCommand]
        private void SwitchToEmployees()
        {
            CurrentViewModel = _employeesCrudeViewModel;
        }

        [RelayCommand]
        private void SwitchToCars() {
            CurrentViewModel = _carsCrudeViewModel;
        }
        [RelayCommand]
        private void SwitchToRentals() {
            CurrentViewModel = _rentalsCrudeViewModel;
        }

        [RelayCommand]
        private void SwitchToPayments()
        {
            CurrentViewModel = _paymentsCrudeViewModel;
        }
        [RelayCommand]
        private void SwitchToMaintences()
        {
            CurrentViewModel = _maintencesCrudeViewModel;
        }
    }
}
