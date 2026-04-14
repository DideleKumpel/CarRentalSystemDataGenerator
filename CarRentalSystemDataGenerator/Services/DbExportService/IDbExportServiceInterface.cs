using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalSystemDataGenerator.Services.DbExportService
{
    internal interface IDbExportServiceInterface
    {
        public string ExportToXml();
        public string ExportToJSON();
    }
}
