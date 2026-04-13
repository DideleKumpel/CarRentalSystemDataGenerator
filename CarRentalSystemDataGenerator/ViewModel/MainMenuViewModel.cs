using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CarRentalSystemDataGenerator.ViewModel;
using CarRentalSystemDataGenerator.View;

namespace CarRentalSystemDataGenerator.ViewModel
{
    internal partial class MainMenuViewModel: ObservableObject
    {
        [RelayCommand]
        private void SwitchToGeneratorView()
        {
            var generatorView = new GeneratorView();
            App.Current.MainWindow.Content = generatorView;
        }
    }
}
