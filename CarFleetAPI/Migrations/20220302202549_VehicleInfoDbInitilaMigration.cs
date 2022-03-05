using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarFleetAPI.Migrations
{
    public partial class VehicleInfoDbInitilaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ProductionYear = table.Column<int>(type: "INTEGER", nullable: true),
                    LoadCapacity = table.Column<int>(type: "INTEGER", nullable: true),
                    CurrentX = table.Column<decimal>(type: "TEXT", nullable: true),
                    CurrentY = table.Column<decimal>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "CurrentX", "CurrentY", "Description", "LoadCapacity", "Model", "ProductionYear", "RegistrationNumber" },
                values: new object[] { 1, null, null, null, null, "Mercedes", null, "ST-111-AA" });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "CurrentX", "CurrentY", "Description", "LoadCapacity", "Model", "ProductionYear", "RegistrationNumber" },
                values: new object[] { 2, null, null, null, null, "Volovo", null, "ST-222-BB" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
