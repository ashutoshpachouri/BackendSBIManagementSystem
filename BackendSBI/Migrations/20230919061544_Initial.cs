using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendSBI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    FullName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FathersName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AadharNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    DOB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RPincode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PPincode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccupationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Income = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnnualIncome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.FullName);
                });

            migrationBuilder.CreateTable(
                name: "InternetBankings",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternetBankings", x => x.Email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "InternetBankings");
        }
    }
}
