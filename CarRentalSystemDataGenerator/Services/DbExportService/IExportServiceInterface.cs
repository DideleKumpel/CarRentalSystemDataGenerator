using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalSystemDataGenerator.Services.DbExportService
{
    internal interface IExportServiceInterface<T>
    {
        public Task<string> ExportToJSONAsync(List<T> items, string filePath);
    }
}
