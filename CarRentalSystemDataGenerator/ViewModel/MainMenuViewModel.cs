using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CarRentalSystemDataGenerator.ViewModel
{
    internal partial class MainMenuViewModel: ObservableObject
    {
        public MainMenuViewModel()
        {
        }

        [RelayCommand]
        private void SwitchToGeneratorView()
        {
            var generatorView = ((App)Application.Current).ServiceProvider.GetService<GeneratorViewModel>();
            App.Current.MainWindow.Content = generatorView;
        }

        [RelayCommand]
        private void SwitchToExportImportView()
        {
            var exportView = ((App)Application.Current).ServiceProvider.GetService<ExportViewModel>();
            App.Current.MainWindow.Content = exportView;
        }
    }
}
