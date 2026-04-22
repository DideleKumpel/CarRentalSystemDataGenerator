using CsvHelper.Configuration;
using System.Globalization;

namespace CarRentalSystemDataGenerator.Services.DbExportService
{
    internal abstract class ExportServiceBase<T> : IExportServiceInterface<T>
    {
        protected readonly CsvConfiguration _csvConfiguration;

        public ExportServiceBase()
        {
        }

        public abstract Task<string> ExportToJSONAsync(List<T> items, string filePath);
    }
}
