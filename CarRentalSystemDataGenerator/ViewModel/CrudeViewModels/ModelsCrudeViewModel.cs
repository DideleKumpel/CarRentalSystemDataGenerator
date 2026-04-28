using CarRentalSystemDataGenerator.DB.Entities;
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
    internal partial class ModelsCrudeViewModel : ObservableObject
    {
        private readonly IDbServiceInterface<Model> _dbService;

        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private int _brandId;
        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private ObservableCollection<Model> _list;
        [ObservableProperty]
        private Model _selected;

        partial void OnSelectedChanged(Model value)
        {
            if (value != null)
            {
                Id = value.ModelID;
                BrandId = value.BrandID;
                Name = value.Name;
                RemoveCommand.NotifyCanExecuteChanged();
                UpdateCommand.NotifyCanExecuteChanged();
            }
        }

        public ModelsCrudeViewModel(IDbServiceInterface<Model> dbService)
        {
            _dbService = dbService;
            Load();
        }

        [RelayCommand]
        private void Load()
        {
            List<Model> list = _dbService.GetAll();
            List = new ObservableCollection<Model>(list);
        }

        [RelayCommand]
        public void Add()
        {
            Model toAdd = new Model
            {
                BrandID = BrandId,
                Name = Name,
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
                var updated = new Model
                {
                    ModelID = Selected.ModelID,
                    BrandID = BrandId,
                    Name = Name,
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
