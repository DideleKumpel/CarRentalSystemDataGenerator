using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CarRentalSystemDataGenerator.Services.DbExportService
{
    internal class ExportServiceBase<T> : IExportServiceInterface<T>
    {
        protected JsonSerializerOptions jsonSerializerOptions;

        public ExportServiceBase(JsonSerializerOptions options)
        {
            jsonSerializerOptions = options;
        }

        public ExportServiceBase()
        {
            jsonSerializerOptions = new JsonSerializerOptions();
        }

        public async Task<string> ExportToJSONAsync(List<T> items, string filePath)
        {
            if(items == null || items.Count == 0)
            {
                throw new ArgumentException("The list of items to export cannot be null or empty.");
            }
            string jsonString = JsonSerializer.Serialize(items, jsonSerializerOptions);
            await File.WriteAllTextAsync(filePath, jsonString);
            return filePath;
        }
    }
}
