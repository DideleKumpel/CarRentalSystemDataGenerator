using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CarRentalSystemDataGenerator.ViewModel
{
    internal partial class ExportViewModel: ObservableObject
    {
        [ObservableProperty]
        private string _directory;
        [ObservableProperty]
        private string _fileName;
        
    }
}
