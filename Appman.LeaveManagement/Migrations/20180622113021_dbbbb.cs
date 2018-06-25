using Microsoft.EntityFrameworkCore.Migrations;

namespace Appman.LeaveManagement.Migrations
{
    public partial class dbbbb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveInfo",
                table: "LeaveInfo");

            migrationBuilder.RenameTable(
                name: "LeaveInfo",
                newName: "LeaveInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveInfos",
                table: "LeaveInfos",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveInfos",
                table: "LeaveInfos");

            migrationBuilder.RenameTable(
                name: "LeaveInfos",
                newName: "LeaveInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveInfo",
                table: "LeaveInfo",
                column: "Id");
        }
    }
}
