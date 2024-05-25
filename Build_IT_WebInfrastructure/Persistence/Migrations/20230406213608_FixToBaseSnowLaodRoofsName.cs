using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Build_IT_WebInfrastructure.Persistence.Migrations
{
    public partial class FixToBaseSnowLaodRoofsName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseSnowLoadRoof_SnowLoads_SnowLoadId",
                table: "BaseSnowLoadRoof");

            migrationBuilder.DropForeignKey(
                name: "FK_CylindricalRoofs_BaseSnowLoadRoof_Id",
                schema: "SnowLoads",
                table: "CylindricalRoofs");

            migrationBuilder.DropForeignKey(
                name: "FK_DriftingsAtProjectionsObstructions_BaseSnowLoadRoof_Id",
                schema: "SnowLoads",
                table: "DriftingsAtProjectionsObstructions");

            migrationBuilder.DropForeignKey(
                name: "FK_MonopitchRoofs_BaseSnowLoadRoof_Id",
                schema: "SnowLoads",
                table: "MonopitchRoofs");

            migrationBuilder.DropForeignKey(
                name: "FK_MultiSpanRoofs_BaseSnowLoadRoof_Id",
                schema: "SnowLoads",
                table: "MultiSpanRoofs");

            migrationBuilder.DropForeignKey(
                name: "FK_PitchedRoofs_BaseSnowLoadRoof_Id",
                schema: "SnowLoads",
                table: "PitchedRoofs");

            migrationBuilder.DropForeignKey(
                name: "FK_RoofsAbuttingToTallerConstructions_BaseSnowLoadRoof_Id",
                schema: "SnowLoads",
                table: "RoofsAbuttingToTallerConstructions");

            migrationBuilder.DropForeignKey(
                name: "FK_Snowguards_BaseSnowLoadRoof_Id",
                schema: "SnowLoads",
                table: "Snowguards");

            migrationBuilder.DropForeignKey(
                name: "FK_SnowOverhangings_BaseSnowLoadRoof_Id",
                schema: "SnowLoads",
                table: "SnowOverhangings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseSnowLoadRoof",
                table: "BaseSnowLoadRoof");

            migrationBuilder.RenameTable(
                name: "BaseSnowLoadRoof",
                newName: "BaseSnowLoadRoofs",
                newSchema: "SnowLoads");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSnowLoadRoof_SnowLoadId",
                schema: "SnowLoads",
                table: "BaseSnowLoadRoofs",
                newName: "IX_BaseSnowLoadRoofs_SnowLoadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseSnowLoadRoofs",
                schema: "SnowLoads",
                table: "BaseSnowLoadRoofs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseSnowLoadRoofs_SnowLoads_SnowLoadId",
                schema: "SnowLoads",
                table: "BaseSnowLoadRoofs",
                column: "SnowLoadId",
                principalSchema: "SnowLoads",
                principalTable: "SnowLoads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CylindricalRoofs_BaseSnowLoadRoofs_Id",
                schema: "SnowLoads",
                table: "CylindricalRoofs",
                column: "Id",
                principalSchema: "SnowLoads",
                principalTable: "BaseSnowLoadRoofs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DriftingsAtProjectionsObstructions_BaseSnowLoadRoofs_Id",
                schema: "SnowLoads",
                table: "DriftingsAtProjectionsObstructions",
                column: "Id",
                principalSchema: "SnowLoads",
                principalTable: "BaseSnowLoadRoofs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MonopitchRoofs_BaseSnowLoadRoofs_Id",
                schema: "SnowLoads",
                table: "MonopitchRoofs",
                column: "Id",
                principalSchema: "SnowLoads",
                principalTable: "BaseSnowLoadRoofs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiSpanRoofs_BaseSnowLoadRoofs_Id",
                schema: "SnowLoads",
                table: "MultiSpanRoofs",
                column: "Id",
                principalSchema: "SnowLoads",
                principalTable: "BaseSnowLoadRoofs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PitchedRoofs_BaseSnowLoadRoofs_Id",
                schema: "SnowLoads",
                table: "PitchedRoofs",
                column: "Id",
                principalSchema: "SnowLoads",
                principalTable: "BaseSnowLoadRoofs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoofsAbuttingToTallerConstructions_BaseSnowLoadRoofs_Id",
                schema: "SnowLoads",
                table: "RoofsAbuttingToTallerConstructions",
                column: "Id",
                principalSchema: "SnowLoads",
                principalTable: "BaseSnowLoadRoofs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Snowguards_BaseSnowLoadRoofs_Id",
                schema: "SnowLoads",
                table: "Snowguards",
                column: "Id",
                principalSchema: "SnowLoads",
                principalTable: "BaseSnowLoadRoofs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SnowOverhangings_BaseSnowLoadRoofs_Id",
                schema: "SnowLoads",
                table: "SnowOverhangings",
                column: "Id",
                principalSchema: "SnowLoads",
                principalTable: "BaseSnowLoadRoofs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseSnowLoadRoofs_SnowLoads_SnowLoadId",
                schema: "SnowLoads",
                table: "BaseSnowLoadRoofs");

            migrationBuilder.DropForeignKey(
                name: "FK_CylindricalRoofs_BaseSnowLoadRoofs_Id",
                schema: "SnowLoads",
                table: "CylindricalRoofs");

            migrationBuilder.DropForeignKey(
                name: "FK_DriftingsAtProjectionsObstructions_BaseSnowLoadRoofs_Id",
                schema: "SnowLoads",
                table: "DriftingsAtProjectionsObstructions");

            migrationBuilder.DropForeignKey(
                name: "FK_MonopitchRoofs_BaseSnowLoadRoofs_Id",
                schema: "SnowLoads",
                table: "MonopitchRoofs");

            migrationBuilder.DropForeignKey(
                name: "FK_MultiSpanRoofs_BaseSnowLoadRoofs_Id",
                schema: "SnowLoads",
                table: "MultiSpanRoofs");

            migrationBuilder.DropForeignKey(
                name: "FK_PitchedRoofs_BaseSnowLoadRoofs_Id",
                schema: "SnowLoads",
                table: "PitchedRoofs");

            migrationBuilder.DropForeignKey(
                name: "FK_RoofsAbuttingToTallerConstructions_BaseSnowLoadRoofs_Id",
                schema: "SnowLoads",
                table: "RoofsAbuttingToTallerConstructions");

            migrationBuilder.DropForeignKey(
                name: "FK_Snowguards_BaseSnowLoadRoofs_Id",
                schema: "SnowLoads",
                table: "Snowguards");

            migrationBuilder.DropForeignKey(
                name: "FK_SnowOverhangings_BaseSnowLoadRoofs_Id",
                schema: "SnowLoads",
                table: "SnowOverhangings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseSnowLoadRoofs",
                schema: "SnowLoads",
                table: "BaseSnowLoadRoofs");

            migrationBuilder.RenameTable(
                name: "BaseSnowLoadRoofs",
                schema: "SnowLoads",
                newName: "BaseSnowLoadRoof");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSnowLoadRoofs_SnowLoadId",
                table: "BaseSnowLoadRoof",
                newName: "IX_BaseSnowLoadRoof_SnowLoadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseSnowLoadRoof",
                table: "BaseSnowLoadRoof",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseSnowLoadRoof_SnowLoads_SnowLoadId",
                table: "BaseSnowLoadRoof",
                column: "SnowLoadId",
                principalSchema: "SnowLoads",
                principalTable: "SnowLoads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CylindricalRoofs_BaseSnowLoadRoof_Id",
                schema: "SnowLoads",
                table: "CylindricalRoofs",
                column: "Id",
                principalTable: "BaseSnowLoadRoof",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DriftingsAtProjectionsObstructions_BaseSnowLoadRoof_Id",
                schema: "SnowLoads",
                table: "DriftingsAtProjectionsObstructions",
                column: "Id",
                principalTable: "BaseSnowLoadRoof",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MonopitchRoofs_BaseSnowLoadRoof_Id",
                schema: "SnowLoads",
                table: "MonopitchRoofs",
                column: "Id",
                principalTable: "BaseSnowLoadRoof",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiSpanRoofs_BaseSnowLoadRoof_Id",
                schema: "SnowLoads",
                table: "MultiSpanRoofs",
                column: "Id",
                principalTable: "BaseSnowLoadRoof",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PitchedRoofs_BaseSnowLoadRoof_Id",
                schema: "SnowLoads",
                table: "PitchedRoofs",
                column: "Id",
                principalTable: "BaseSnowLoadRoof",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoofsAbuttingToTallerConstructions_BaseSnowLoadRoof_Id",
                schema: "SnowLoads",
                table: "RoofsAbuttingToTallerConstructions",
                column: "Id",
                principalTable: "BaseSnowLoadRoof",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Snowguards_BaseSnowLoadRoof_Id",
                schema: "SnowLoads",
                table: "Snowguards",
                column: "Id",
                principalTable: "BaseSnowLoadRoof",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SnowOverhangings_BaseSnowLoadRoof_Id",
                schema: "SnowLoads",
                table: "SnowOverhangings",
                column: "Id",
                principalTable: "BaseSnowLoadRoof",
                principalColumn: "Id");
        }
    }
}
