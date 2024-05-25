using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Build_IT_WebInfrastructure.Persistence.Migrations
{
    public partial class ProjectDesignerClaim_DesignerIdAsPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDesignerClaims_Designers_DesignerId",
                schema: "Projects",
                table: "ProjectDesignerClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectDesignerClaims",
                schema: "Projects",
                table: "ProjectDesignerClaims");

            migrationBuilder.DropIndex(
                name: "IX_ProjectDesignerClaims_DesignerId",
                schema: "Projects",
                table: "ProjectDesignerClaims");

            migrationBuilder.AlterColumn<string>(
                name: "DesignerId",
                schema: "Projects",
                table: "ProjectDesignerClaims",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectDesignerClaims",
                schema: "Projects",
                table: "ProjectDesignerClaims",
                columns: new[] { "DesignerId", "ProjectId", "ClaimId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDesignerClaims_ProjectId",
                schema: "Projects",
                table: "ProjectDesignerClaims",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDesignerClaims_Designers_DesignerId",
                schema: "Projects",
                table: "ProjectDesignerClaims",
                column: "DesignerId",
                principalSchema: "Projects",
                principalTable: "Designers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDesignerClaims_Designers_DesignerId",
                schema: "Projects",
                table: "ProjectDesignerClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectDesignerClaims",
                schema: "Projects",
                table: "ProjectDesignerClaims");

            migrationBuilder.DropIndex(
                name: "IX_ProjectDesignerClaims_ProjectId",
                schema: "Projects",
                table: "ProjectDesignerClaims");

            migrationBuilder.AlterColumn<string>(
                name: "DesignerId",
                schema: "Projects",
                table: "ProjectDesignerClaims",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectDesignerClaims",
                schema: "Projects",
                table: "ProjectDesignerClaims",
                columns: new[] { "ProjectId", "ClaimId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDesignerClaims_DesignerId",
                schema: "Projects",
                table: "ProjectDesignerClaims",
                column: "DesignerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDesignerClaims_Designers_DesignerId",
                schema: "Projects",
                table: "ProjectDesignerClaims",
                column: "DesignerId",
                principalSchema: "Projects",
                principalTable: "Designers",
                principalColumn: "UserId");
        }
    }
}
