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
    internal class AddressDbExportService : DbExportServiceBase
    {
        public AddressDbExportService(AppDbContext db) : base(db)
        {
        }

        public override string ExportToXml()
        {
            List<Address> addresses = new List<Address>();
            addresses = _db.Addresses.ToList();
            var xmlSerializer = new XmlSerializer(typeof(List<Address>));
            StringWriter stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, addresses);
            return stringWriter.ToString();
        }

        public override string ExportToJSON()
        {
            List<Address> addresses = new List<Address>();
            addresses = _db.Addresses.ToList();
            var JsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(addresses, JsonOptions);
            return jsonString;
        }
    }
}
