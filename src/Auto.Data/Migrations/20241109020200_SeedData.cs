using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auto.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Path to the SQL file
            //var userFilePath = Path.Combine("Scripts", "UsersSeedData.sql");
            var userFilePath = Path.Combine(AppContext.BaseDirectory, "Scripts", "UsersSeedData.sql");

            if (!File.Exists(userFilePath))
            {
                throw new FileNotFoundException("SQL file not found", userFilePath);
            }

            // Read the SQL file content
            var sqlCommands = File.ReadAllText(userFilePath);

            // Execute the SQL commands
            migrationBuilder.Sql(sqlCommands);

            // Path to the SQL file
            var autoFilePath = Path.Combine(AppContext.BaseDirectory, "Scripts", "AutoSeedData.sql");

            if (!File.Exists(autoFilePath))
            {
                throw new FileNotFoundException("SQL file not found", autoFilePath);
            }

            // Read the SQL file content
            sqlCommands = File.ReadAllText(autoFilePath);

            // Execute the SQL commands
            migrationBuilder.Sql(sqlCommands);


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}


//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace Auto.Data.Migrations
//{
//    /// <inheritdoc />
//    public partial class SeedData : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {

//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {

//        }
//    }
//}
