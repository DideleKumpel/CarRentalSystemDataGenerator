using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalSystemDataGenerator.ViewModel
{
    internal partial class GeneratorViewModel: ObservableObject
    {
        public GeneratorViewModel() { }

        //Generator flags
        [ObservableProperty]
        private bool _adressesIsChecked;
        [ObservableProperty]
        private bool _brandsIsChecked;
        [ObservableProperty]
        private bool _carsIsChecked;
        [ObservableProperty]
        private bool _customersIsChecked;
        [ObservableProperty]
        private bool _employeesIsChecked;
        [ObservableProperty]
        private bool _maintenancesIsChecked;
        [ObservableProperty]
        private bool _modelsIsChecked;
        [ObservableProperty]
        private bool _officesIsChecked;
        [ObservableProperty]
        private bool _paymentsIsChecked;
        [ObservableProperty]
        private bool _rentalsIsChecked;

        [ObservableProperty]
        private int _numOfRecordToGenreate;


    }
}
