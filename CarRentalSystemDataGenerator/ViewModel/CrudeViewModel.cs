using CarRentalSystemDataGenerator.View;
using CarRentalSystemDataGenerator.ViewModels.CrudeViewModel;
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

        public CrudeViewModel()
        {
            var customersCrudeViewModel = ((App)Application.Current).ServiceProvider.GetService<CustomersCrudeViewModel>();
            CurrentViewModel = customersCrudeViewModel;
            customersCrudeViewModel.LoadCustomersCommand.Execute(null);
        }

        [RelayCommand]
        private void BackToMainWindow()
        {
            App.Current.MainWindow.Content = new MainMenuView();
        }
    }
}
