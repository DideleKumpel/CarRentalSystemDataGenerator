using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalSystemDataGenerator.Services.DbExportService
{
    internal interface IDbExportServiceInterface
    {
        public void ExportToCSV(string filePath);
        public void ExportToJSON(string filePath);
    }
}
