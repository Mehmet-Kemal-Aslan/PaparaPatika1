using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace PaparaPatika.Entitities
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PaparaDbContext>
    {
        public PaparaDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("PaparaDB");
            var optionsBuilder = new DbContextOptionsBuilder<PaparaDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new PaparaDbContext(optionsBuilder.Options);
        }
    }
}
