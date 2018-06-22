using Microsoft.EntityFrameworkCore.Migrations;

namespace Appman.LeaveManagement.Migrations
{
    public partial class gg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_reportings",
                table: "reportings");

            migrationBuilder.RenameTable(
                name: "reportings",
                newName: "Reportings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reportings",
                table: "Reportings",
                columns: new[] { "EmployeeId", "ReportingTo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reportings",
                table: "Reportings");

            migrationBuilder.RenameTable(
                name: "Reportings",
                newName: "reportings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reportings",
                table: "reportings",
                columns: new[] { "EmployeeId", "ReportingTo" });
        }
    }
}
