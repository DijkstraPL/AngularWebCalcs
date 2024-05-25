using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Build_IT_WebInfrastructure.Persistence.Migrations
{
    public partial class UserInfoForDeadLoads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "Projects",
                table: "DeadLoads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Projects",
                table: "DeadLoads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                schema: "Projects",
                table: "DeadLoads",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Projects",
                table: "DeadLoads",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                schema: "Projects",
                table: "DeadLoads");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Projects",
                table: "DeadLoads");

            migrationBuilder.DropColumn(
                name: "LastModified",
                schema: "Projects",
                table: "DeadLoads");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Projects",
                table: "DeadLoads");
        }
    }
}
