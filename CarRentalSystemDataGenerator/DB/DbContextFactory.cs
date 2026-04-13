using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace CarRentalSystemDataGenerator.DB
{
    public static class DbContextFactory
    {
        public static AppDbContext CreateFromAppSettings()
        {
            var baseDir = Directory.GetCurrentDirectory();
            baseDir = baseDir.Replace("\\bin\\Debug\\net10.0-windows", ""); // Adjust for typical build output path

            // Try common locations for appsettings.json. EF tools may run with a different working directory,
            // so search the current directory and its subdirectories for the file.
            var path = Path.Combine(baseDir, "appsettings.json");
            if (!File.Exists(path))
            {
                path = Directory.EnumerateFiles(baseDir, "appsettings.json", SearchOption.AllDirectories).FirstOrDefault();
            }

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                throw new FileNotFoundException($"Configuration file not found: appsettings.json (searched under {baseDir})");

            var json = File.ReadAllText(path);
            using var doc = JsonDocument.Parse(json);
            string? conn = null;
            if (doc.RootElement.TryGetProperty("ConnectionStrings", out var cs)
                && cs.TryGetProperty("DefaultConnection", out var dc))
            {
                conn = dc.GetString();
            }

            if (string.IsNullOrEmpty(conn))
                throw new InvalidOperationException("DefaultConnection not found in appsettings.json");

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(conn)
                .Options;

            return new AppDbContext(options);
        }
    }
}
