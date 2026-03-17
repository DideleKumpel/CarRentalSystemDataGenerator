using Microsoft.EntityFrameworkCore.Design;

namespace CarRentalSystemDataGenerator.DB
{
    // Design-time factory used by EF Core tools to create the DbContext
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            return DbContextFactory.CreateFromAppSettings();
        }
    }
}
