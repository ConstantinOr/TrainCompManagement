using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TrainCompManagement.DAL;

public class TrainCompManagementDbContextFactory:  IDesignTimeDbContextFactory<TrainCompManagementDbContext>
{
    public TrainCompManagementDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<TrainCompManagementDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new TrainCompManagementDbContext(optionsBuilder.Options); 
    }
}