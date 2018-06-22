using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Appman.LeaveManagement.Migrations
{
    public partial class add_LeaveInfo3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "startHours",
                table: "LeaveInfo",
                newName: "HoursStartDate");

            migrationBuilder.RenameColumn(
                name: "startDate",
                table: "LeaveInfo",
                newName: "startDateTime");

            migrationBuilder.RenameColumn(
                name: "endHours",
                table: "LeaveInfo",
                newName: "HoursEndDate");

            migrationBuilder.RenameColumn(
                name: "endDate",
                table: "LeaveInfo",
                newName: "endDateTime");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Employees",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Employees",
                newName: "StartWorkingDate");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Employees",
                newName: "Position");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                table: "LeaveInfo",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "AprroveTime",
                table: "LeaveInfo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "AttachedFile",
                table: "LeaveInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "LeaveInfo",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "LeaveInfo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Employees",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "LeaveInfo");

            migrationBuilder.DropColumn(
                name: "AprroveTime",
                table: "LeaveInfo");

            migrationBuilder.DropColumn(
                name: "AttachedFile",
                table: "LeaveInfo");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "LeaveInfo");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LeaveInfo");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "startDateTime",
                table: "LeaveInfo",
                newName: "startDate");

            migrationBuilder.RenameColumn(
                name: "endDateTime",
                table: "LeaveInfo",
                newName: "endDate");

            migrationBuilder.RenameColumn(
                name: "HoursStartDate",
                table: "LeaveInfo",
                newName: "startHours");

            migrationBuilder.RenameColumn(
                name: "HoursEndDate",
                table: "LeaveInfo",
                newName: "endHours");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Employees",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "StartWorkingDate",
                table: "Employees",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Employees",
                newName: "Role");
        }
    }
}
