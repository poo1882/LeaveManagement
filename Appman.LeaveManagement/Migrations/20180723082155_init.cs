using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Appman.LeaveManagement.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Approbations",
                columns: table => new
                {
                    ApprobationGuid = table.Column<Guid>(nullable: false),
                    LeaveId = table.Column<int>(nullable: false),
                    ApproverId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approbations", x => x.ApprobationGuid);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    StaffId = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ProfilePicture = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    StartWorkingDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Section = table.Column<string>(nullable: true),
                    IsInProbation = table.Column<bool>(nullable: false),
                    GenderCode = table.Column<string>(nullable: true),
                    IsSuperHr = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "LeaveInfos",
                columns: table => new
                {
                    LeaveId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(nullable: true),
                    StaffId = table.Column<string>(nullable: true),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    HoursStartDate = table.Column<int>(nullable: false),
                    HoursEndDate = table.Column<int>(nullable: false),
                    ApprovalStatus = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    ApprovedTime = table.Column<DateTime>(nullable: true),
                    ApprovedBy = table.Column<string>(nullable: true),
                    AttachedFile1 = table.Column<string>(nullable: true),
                    AttachedFileName1 = table.Column<string>(nullable: true),
                    AttachedFile2 = table.Column<string>(nullable: true),
                    AttachedFileName2 = table.Column<string>(nullable: true),
                    AttachedFile3 = table.Column<string>(nullable: true),
                    AttachedFileName3 = table.Column<string>(nullable: true),
                    RequestedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveInfos", x => x.LeaveId);
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
                    StaffId = table.Column<string>(nullable: false),
                    Year = table.Column<string>(nullable: false),
                    AnnualHours = table.Column<int>(nullable: false),
                    SickHours = table.Column<int>(nullable: false),
                    LWPHours = table.Column<int>(nullable: false),
                    TotalAnnualHours = table.Column<int>(nullable: false),
                    TotalSickHours = table.Column<int>(nullable: false),
                    TotalLWPHours = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemainingHours", x => new { x.StaffId, x.Year });
                });

            migrationBuilder.CreateTable(
                name: "Reportings",
                columns: table => new
                {
                    StaffId = table.Column<string>(nullable: false),
                    Approver = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportings", x => new { x.StaffId, x.Approver });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Approbations");

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
