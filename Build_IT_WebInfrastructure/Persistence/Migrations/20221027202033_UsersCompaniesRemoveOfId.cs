using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Build_IT_WebInfrastructure.Persistence.Migrations
{
    public partial class UsersCompaniesRemoveOfId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                schema: "Projects",
                table: "UserCompanies");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Projects",
                table: "UserCompanies");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Projects",
                table: "UserCompanies");

            migrationBuilder.DropColumn(
                name: "LastModified",
                schema: "Projects",
                table: "UserCompanies");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Projects",
                table: "UserCompanies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "Projects",
                table: "UserCompanies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Projects",
                table: "UserCompanies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "Projects",
                table: "UserCompanies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                schema: "Projects",
                table: "UserCompanies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Projects",
                table: "UserCompanies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
