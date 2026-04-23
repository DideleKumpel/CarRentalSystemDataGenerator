using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace CarRentalSystemDataGenerator.Services.ImportService
{
    internal class AddressImportService : ImportServiceBase<Address>
    {
        public AddressImportService(): base() { }
        public AddressImportService(JsonSerializerOptions options) : base(options) { }

        public override async Task<List<Address>> ImportFromJsonAsync(string filePath)
        {
            if(!await CanImportFromJsonAsync(filePath))
                throw new InvalidDataException("The provided JSON file is not valid for importing Address data.");
            return await base.ImportFromJsonAsync(filePath);
        }

        public override async Task<bool> CanImportFromJsonAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                return false;

            try
            {
                using var stream = File.OpenRead(filePath);
                using var doc = await JsonDocument.ParseAsync(stream);

                JsonElement root = doc.RootElement;
                JsonElement elementToCheck = root.ValueKind == JsonValueKind.Array
                    ? (root.GetArrayLength() > 0 ? root[0] : default)
                    : root;

                if (elementToCheck.ValueKind == JsonValueKind.Undefined) return true;

                var expectedProps = typeof(Address).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Select(p => p.Name.ToLower());

                foreach (var propName in expectedProps)
                {
                    bool exists = elementToCheck.EnumerateObject()
                        .Any(jsonProp => jsonProp.Name.Equals(propName, StringComparison.OrdinalIgnoreCase));

                    if (!exists) return false;
                }

                return true;
            }
            catch { return false; }
        }
    }
}
