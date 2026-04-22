using CarRentalSystemDataGenerator.DB;
using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace CarRentalSystemDataGenerator.Services.DbExportService
{
    internal class AddressExportService : ExportServiceBase<Address>
    {
        public AddressExportService()
        {
        }


        public override async Task<string> ExportToJSONAsync(List<Address> items, string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
