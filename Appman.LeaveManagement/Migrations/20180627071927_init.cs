using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Appman.LeaveManagement.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ProfilePicture = table.Column<byte[]>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    StartWorkingDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    HoursStartDate = table.Column<int>(nullable: false),
                    HoursEndDate = table.Column<int>(nullable: false),
                    ApprovalStatus = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    AprroveTime = table.Column<DateTime>(nullable: false),
                    ApprovedBy = table.Column<Guid>(nullable: false),
                    AttachedFile = table.Column<byte[]>(nullable: true),
                    RequestedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveInfos", x => x.Id);
                });

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
                name: "Reportings",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    ReportingTo = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportings", x => new { x.EmployeeId, x.ReportingTo });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "LeaveInfos");

            migrationBuilder.DropTable(
                name: "MdGenders");

            migrationBuilder.DropTable(
                name: "RemainingHours");

            migrationBuilder.DropTable(
                name: "Reportings");
        }
    }
}
