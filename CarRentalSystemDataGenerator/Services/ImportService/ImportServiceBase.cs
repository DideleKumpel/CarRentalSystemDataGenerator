using CarRentalSystemDataGenerator.DB.Entities;
using CarRentalSystemDataGenerator.Services.ImportService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace CarRentalSystemDataGenerator.Services.ImportService
{
    internal class ImportServiceBase<T>: IImportService<T> where T : class
    {
        protected JsonSerializerOptions jsonSerializerOptions;

        public ImportServiceBase(JsonSerializerOptions options)
        {
            jsonSerializerOptions = options;
        }

        public ImportServiceBase()
        {
            jsonSerializerOptions = new JsonSerializerOptions();
        }

        public virtual async Task<List<T>> ImportFromJsonAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified JSON file was not found.", filePath);
            }

            try
            {
                using FileStream openStream = File.OpenRead(filePath);

                List<T> result = await JsonSerializer.DeserializeAsync<List<T>>(openStream, jsonSerializerOptions);

                return result ?? new List<T>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> CanImportFromJsonAsync(string filePath)
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

                var expectedProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
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
