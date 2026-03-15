using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json;

namespace CarRentalSystemDataGenerator.DB
{
    public static class DbContextFactory
    {
        public static AppDbContext CreateFromAppSettings()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "DB", "appsettings.json");
            if (!File.Exists(path))
                throw new FileNotFoundException($"Configuration file not found: {path}");

            var json = File.ReadAllText(path);
            using var doc = JsonDocument.Parse(json);
            string? conn = null;
            if (doc.RootElement.TryGetProperty("ConnectionStrings", out var cs)
                && cs.TryGetProperty("DefaultConnection", out var dc))
            {
                conn = dc.GetString();
            }

            if (string.IsNullOrEmpty(conn))
                throw new InvalidOperationException("DefaultConnection not found in DB/appsettings.json");

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(conn)
                .Options;

            return new AppDbContext(options);
        }
    }
}
