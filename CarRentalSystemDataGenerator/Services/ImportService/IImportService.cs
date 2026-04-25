using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace CarRentalSystemDataGenerator.Services.ImportService
{
    internal interface IImportService<T>
    {
        public Task<List<T>> ImportFromJsonAsync(string filePath);
        public Task<bool> CanImportFromJsonAsync(string filePath);
    }
}