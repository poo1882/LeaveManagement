using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Appman.LeaveManagement.Migrations
{
    public partial class add_5_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MdGenders",
                columns: table => new
                {
                    GenderCode = table.Column<string>(nullable: false),
                    TH = table.Column<string>(nullable: true),
                    EN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MdGenders", x => x.GenderCode);
                });

            migrationBuilder.CreateTable(
                name: "RemainingHours",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    Year = table.Column<string>(nullable: false),
                    AnnualHours = table.Column<int>(nullable: false),
                    SickHours = table.Column<int>(nullable: false),
                    LWPHours = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemainingHours", x => new { x.EmployeeId, x.Year });
                });

            migrationBuilder.CreateTable(
                name: "reportings",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    ReportingTo = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportings", x => new { x.EmployeeId, x.ReportingTo });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MdGenders");

            migrationBuilder.DropTable(
                name: "RemainingHours");

            migrationBuilder.DropTable(
                name: "reportings");
        }
    }
}
