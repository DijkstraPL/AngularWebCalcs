using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Build_IT_WebInfrastructure.Persistence.Migrations
{
    public partial class SnowLoadsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SnowLoads");

            migrationBuilder.CreateTable(
                name: "SnowLoads",
                schema: "SnowLoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(124)", maxLength: 124, nullable: false),
                    SnowLoadRoofId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnowLoads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseSnowLoadRoof",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SnowLoadId = table.Column<int>(type: "int", nullable: false),
                    AltitudeAboveSea = table.Column<double>(type: "float", nullable: true),
                    CurrentZone = table.Column<int>(type: "int", nullable: false),
                    CurrentTopography = table.Column<int>(type: "int", nullable: false),
                    ThermalCoefficient = table.Column<double>(type: "float", nullable: true),
                    OverallHeatTransferCoefficient = table.Column<double>(type: "float", nullable: true),
                    InternalTemperature = table.Column<double>(type: "float", nullable: true),
                    TempreatureDifference = table.Column<double>(type: "float", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseSnowLoadRoof", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseSnowLoadRoof_SnowLoads_SnowLoadId",
                        column: x => x.SnowLoadId,
                        principalSchema: "SnowLoads",
                        principalTable: "SnowLoads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSnowLoads",
                schema: "SnowLoads",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    SnowLoadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSnowLoads", x => new { x.ProjectId, x.SnowLoadId });
                    table.ForeignKey(
                        name: "FK_ProjectSnowLoads_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Projects",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSnowLoads_SnowLoads_SnowLoadId",
                        column: x => x.SnowLoadId,
                        principalSchema: "SnowLoads",
                        principalTable: "SnowLoads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CylindricalRoofs",
                schema: "SnowLoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    DriftLength = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CylindricalRoofs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CylindricalRoofs_BaseSnowLoadRoof_Id",
                        column: x => x.Id,
                        principalTable: "BaseSnowLoadRoof",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DriftingsAtProjectionsObstructions",
                schema: "SnowLoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DriftLength = table.Column<double>(type: "float", nullable: false),
                    ObstructionHeight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriftingsAtProjectionsObstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriftingsAtProjectionsObstructions_BaseSnowLoadRoof_Id",
                        column: x => x.Id,
                        principalTable: "BaseSnowLoadRoof",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MonopitchRoofs",
                schema: "SnowLoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Slope = table.Column<double>(type: "float", nullable: false),
                    SnowFences = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonopitchRoofs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonopitchRoofs_BaseSnowLoadRoof_Id",
                        column: x => x.Id,
                        principalTable: "BaseSnowLoadRoof",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MultiSpanRoofs",
                schema: "SnowLoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LeftSlope = table.Column<double>(type: "float", nullable: false),
                    LeftSnowFences = table.Column<bool>(type: "bit", nullable: false),
                    RightSlope = table.Column<double>(type: "float", nullable: false),
                    RightSnowFences = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiSpanRoofs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultiSpanRoofs_BaseSnowLoadRoof_Id",
                        column: x => x.Id,
                        principalTable: "BaseSnowLoadRoof",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PitchedRoofs",
                schema: "SnowLoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LeftSlope = table.Column<double>(type: "float", nullable: false),
                    LeftSnowFences = table.Column<bool>(type: "bit", nullable: false),
                    RightSlope = table.Column<double>(type: "float", nullable: false),
                    RightSnowFences = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PitchedRoofs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PitchedRoofs_BaseSnowLoadRoof_Id",
                        column: x => x.Id,
                        principalTable: "BaseSnowLoadRoof",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoofsAbuttingToTallerConstructions",
                schema: "SnowLoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    WidthOfUpperBuilding = table.Column<double>(type: "float", nullable: false),
                    WidthOfLowerBuilding = table.Column<bool>(type: "bit", nullable: false),
                    HeightDifference = table.Column<double>(type: "float", nullable: false),
                    SlopeOfHigherRoof = table.Column<bool>(type: "bit", nullable: false),
                    SnowFencesOnHigherRoof = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoofsAbuttingToTallerConstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoofsAbuttingToTallerConstructions_BaseSnowLoadRoof_Id",
                        column: x => x.Id,
                        principalTable: "BaseSnowLoadRoof",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Snowguards",
                schema: "SnowLoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SnowLoadOnRoofValue = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Slope = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snowguards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Snowguards_BaseSnowLoadRoof_Id",
                        column: x => x.Id,
                        principalTable: "BaseSnowLoadRoof",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SnowOverhangings",
                schema: "SnowLoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SnowLayerDepth = table.Column<double>(type: "float", nullable: false),
                    SnowFences = table.Column<bool>(type: "bit", nullable: false),
                    Slope = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnowOverhangings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SnowOverhangings_BaseSnowLoadRoof_Id",
                        column: x => x.Id,
                        principalTable: "BaseSnowLoadRoof",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseSnowLoadRoof_SnowLoadId",
                table: "BaseSnowLoadRoof",
                column: "SnowLoadId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSnowLoads_SnowLoadId",
                schema: "SnowLoads",
                table: "ProjectSnowLoads",
                column: "SnowLoadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CylindricalRoofs",
                schema: "SnowLoads");

            migrationBuilder.DropTable(
                name: "DriftingsAtProjectionsObstructions",
                schema: "SnowLoads");

            migrationBuilder.DropTable(
                name: "MonopitchRoofs",
                schema: "SnowLoads");

            migrationBuilder.DropTable(
                name: "MultiSpanRoofs",
                schema: "SnowLoads");

            migrationBuilder.DropTable(
                name: "PitchedRoofs",
                schema: "SnowLoads");

            migrationBuilder.DropTable(
                name: "ProjectSnowLoads",
                schema: "SnowLoads");

            migrationBuilder.DropTable(
                name: "RoofsAbuttingToTallerConstructions",
                schema: "SnowLoads");

            migrationBuilder.DropTable(
                name: "Snowguards",
                schema: "SnowLoads");

            migrationBuilder.DropTable(
                name: "SnowOverhangings",
                schema: "SnowLoads");

            migrationBuilder.DropTable(
                name: "BaseSnowLoadRoof");

            migrationBuilder.DropTable(
                name: "SnowLoads",
                schema: "SnowLoads");
        }
    }
}
