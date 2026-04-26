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
    internal partial class BrandsCrudeViewModel: ObservableObject
    {
        private readonly IDbServiceInterface<Brand> _dbService;

        [ObservableProperty]
        private ObservableCollection<Brand> _brandsList;
        [ObservableProperty]
        private Brand _brandSelected;

        [ObservableProperty]
        private string _brandName;
        [ObservableProperty]
        private string _brandId;

        public BrandsCrudeViewModel(IDbServiceInterface<Brand> dbService)
        {
            _dbService = dbService;
            LoadBrands();
        }

        [RelayCommand]
        public void LoadBrands()
        {
            List<Brand> list = _dbService.GetAll();
            BrandsList = new ObservableCollection<Brand>(list);
        }

        [RelayCommand]
        private void AddBrand()
        {
            if (string.IsNullOrEmpty(_brandName))
            {
                Brand brandToAdd = new Brand() {
                    Name = BrandName,
                };
                try
                {
                    _dbService.Add(brandToAdd);
                    MessageBox.Show("Brand added");
                }catch
                {
                    MessageBox.Show("Error ocured");
                }
                LoadBrands();
            }
            else
            {
                MessageBox.Show("Fill boxes");
            }
        }

        [RelayCommand(CanExecute = nameof(IsSelected))]
        private void RemoveBrand() {
            if (BrandSelected != null) {
                try
                {
                    _dbService.Delete(BrandSelected);
                    MessageBox.Show("Brand deleted");
                }
                catch {
                    MessageBox.Show("Error");
                }
            }
        }

        [RelayCommand(CanExecute = nameof(IsSelected))]
        private void UpdateBrand()
        {
            if (BrandSelected != null || string.IsNullOrEmpty(BrandName))
            {
                Brand brand = new Brand() { 
                    BrandID = BrandSelected.BrandID,
                    Name = BrandName
                };
                try
                {
                    _dbService.Update(brand);
                    MessageBox.Show("Succes");
                }
                catch {
                    MessageBox.Show("Error");
                }
                LoadBrands();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private bool IsSelected()
        {
            return BrandSelected != null;
        }
    }
}
