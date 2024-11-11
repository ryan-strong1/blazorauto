using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Auto.Data
{
    // Design time tools like migrations use this factory to create the DbContext
    // EX command: dotnet ef migrations add SeedData --project . --startup-project ../Auto.API -- "Sql connection string"
    public class AutoDbContextFactory : IDesignTimeDbContextFactory<AutoDbContext>
    {
        public AutoDbContext CreateDbContext(string[] args)
        {
            string connectionString = "";

            if (args != null && args.Length > 0)
            {
                connectionString = args[0];
            }
            else
            {
                throw new Exception("Connection string is required");
            }

            // Configure DbContext options
            var optionsBuilder = new DbContextOptionsBuilder<AutoDbContext>();
            optionsBuilder.UseSqlServer(connectionString, options =>
            {
                options.UseNetTopologySuite();
                options.EnableRetryOnFailure(
                    maxRetryCount: 5, // Maximum number of retry attempts
                    maxRetryDelay: TimeSpan.FromSeconds(30), // Maximum delay between retries
                    errorNumbersToAdd: null); // Additional error numbers to consider transient
            });

            return new AutoDbContext(optionsBuilder.Options);
        }
    }
}
