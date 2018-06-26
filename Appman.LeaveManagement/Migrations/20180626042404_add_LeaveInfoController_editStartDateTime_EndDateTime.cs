using Microsoft.EntityFrameworkCore.Migrations;

namespace Appman.LeaveManagement.Migrations
{
    public partial class add_LeaveInfoController_editStartDateTime_EndDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "startDateTime",
                table: "LeaveInfos",
                newName: "StartDateTime");

            migrationBuilder.RenameColumn(
                name: "endDateTime",
                table: "LeaveInfos",
                newName: "EndDateTime");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "LeaveInfos",
                newName: "ApprovalStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "LeaveInfos",
                newName: "startDateTime");

            migrationBuilder.RenameColumn(
                name: "EndDateTime",
                table: "LeaveInfos",
                newName: "endDateTime");

            migrationBuilder.RenameColumn(
                name: "ApprovalStatus",
                table: "LeaveInfos",
                newName: "Status");
        }
    }
}
