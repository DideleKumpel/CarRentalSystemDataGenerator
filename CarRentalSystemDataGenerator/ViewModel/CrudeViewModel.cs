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

        public CrudeViewModel()
        {
            _addressesCrudeViewModel = ((App)Application.Current).ServiceProvider.GetService<AddressesCrudeViewModel>();
            _customersCrudeViewModel = ((App)Application.Current).ServiceProvider.GetService<CustomersCrudeViewModel>();
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
    }
}
