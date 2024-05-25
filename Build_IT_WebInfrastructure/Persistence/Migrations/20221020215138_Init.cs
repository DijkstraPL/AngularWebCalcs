using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Build_IT_WebInfrastructure.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DeadLoads");

            migrationBuilder.EnsureSchema(
                name: "Scripts");

            migrationBuilder.EnsureSchema(
                name: "Projects");

            migrationBuilder.EnsureSchema(
                name: "Profiles");

            migrationBuilder.CreateTable(
                name: "Additions",
                schema: "DeadLoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(124)", maxLength: 124, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Additions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Application_Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Flags = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "DeadLoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                schema: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeadLoads",
                schema: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeadLoads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Designers",
                schema: "Projects",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "Figures",
                schema: "Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Figures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Use = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Algorithm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsX509Certificate = table.Column<bool>(type: "bit", nullable: false),
                    DataProtected = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "ProfileTypes",
                schema: "Profiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScriptDatas",
                schema: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScriptDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScriptGroups",
                schema: "Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScriptGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                schema: "Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitGroups",
                schema: "Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Application_Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Application_Tokens_Application_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Application_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subcategories",
                schema: "DeadLoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "DeadLoads",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Projects",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                schema: "Profiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ProfileTypeId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parameters_ProfileTypes_ProfileTypeId",
                        column: x => x.ProfileTypeId,
                        principalSchema: "Profiles",
                        principalTable: "ProfileTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SectionPoints",
                schema: "Profiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Y = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    ChamferType = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ChamferX = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    ChamferY = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    ProfileTypeId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionPoints_ProfileTypes_ProfileTypeId",
                        column: x => x.ProfileTypeId,
                        principalSchema: "Profiles",
                        principalTable: "ProfileTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SteelProfiles",
                schema: "Profiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ProfileTypeId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteelProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SteelProfiles_ProfileTypes_ProfileTypeId",
                        column: x => x.ProfileTypeId,
                        principalSchema: "Profiles",
                        principalTable: "ProfileTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Scripts",
                schema: "Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<int>(type: "int", maxLength: 255, nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Added = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccordingTo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    PreviousScriptId = table.Column<int>(type: "int", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scripts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scripts_ScriptGroups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Scripts",
                        principalTable: "ScriptGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Scripts_Scripts_PreviousScriptId",
                        column: x => x.PreviousScriptId,
                        principalSchema: "Scripts",
                        principalTable: "Scripts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitGroupTranslations",
                schema: "Scripts",
                columns: table => new
                {
                    UnitGroupId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitGroupTranslations", x => new { x.UnitGroupId, x.Language });
                    table.ForeignKey(
                        name: "FK_UnitGroupTranslations_UnitGroups_UnitGroupId",
                        column: x => x.UnitGroupId,
                        principalSchema: "Scripts",
                        principalTable: "UnitGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                schema: "Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    UnitGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_UnitGroups_UnitGroupId",
                        column: x => x.UnitGroupId,
                        principalSchema: "Scripts",
                        principalTable: "UnitGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                schema: "DeadLoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MinimumDensity = table.Column<double>(type: "float", nullable: false),
                    MaximumDensity = table.Column<double>(type: "float", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubcategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_Subcategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalSchema: "DeadLoads",
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDeadLoads",
                schema: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    DeadLoadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDeadLoads", x => new { x.ProjectId, x.DeadLoadId });
                    table.ForeignKey(
                        name: "FK_ProjectDeadLoads_DeadLoads_DeadLoadId",
                        column: x => x.DeadLoadId,
                        principalSchema: "Projects",
                        principalTable: "DeadLoads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectDeadLoads_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Projects",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDesignerClaims",
                schema: "Projects",
                columns: table => new
                {
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    DesignerId = table.Column<int>(type: "int", nullable: false),
                    DesignerUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDesignerClaims", x => new { x.DesignerId, x.ProjectId, x.ClaimId });
                    table.ForeignKey(
                        name: "FK_ProjectDesignerClaims_Claims_ClaimId",
                        column: x => x.ClaimId,
                        principalSchema: "Projects",
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectDesignerClaims_Designers_DesignerUserId",
                        column: x => x.DesignerUserId,
                        principalSchema: "Projects",
                        principalTable: "Designers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectDesignerClaims_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Projects",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParameterValues",
                schema: "Profiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParameterId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    SteelProfileId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParameterValues_Parameters_ParameterId",
                        column: x => x.ParameterId,
                        principalSchema: "Profiles",
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParameterValues_SteelProfiles_SteelProfileId",
                        column: x => x.SteelProfileId,
                        principalSchema: "Profiles",
                        principalTable: "SteelProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ParameterGroups",
                schema: "Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    VisibilityValidator = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ScriptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParameterGroups_Scripts_ScriptId",
                        column: x => x.ScriptId,
                        principalSchema: "Scripts",
                        principalTable: "Scripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectScripts",
                schema: "Projects",
                columns: table => new
                {
                    ScriptId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectScripts", x => new { x.ScriptId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectScripts_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Projects",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectScripts_Scripts_ScriptId",
                        column: x => x.ScriptId,
                        principalSchema: "Scripts",
                        principalTable: "Scripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScriptFigures",
                schema: "Scripts",
                columns: table => new
                {
                    ScriptId = table.Column<int>(type: "int", nullable: false),
                    FigureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScriptFigures", x => new { x.ScriptId, x.FigureId });
                    table.ForeignKey(
                        name: "FK_ScriptFigures_Figures_FigureId",
                        column: x => x.FigureId,
                        principalSchema: "Scripts",
                        principalTable: "Figures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScriptFigures_Scripts_ScriptId",
                        column: x => x.ScriptId,
                        principalSchema: "Scripts",
                        principalTable: "Scripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScriptsTranslations",
                schema: "Scripts",
                columns: table => new
                {
                    ScriptId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScriptsTranslations", x => new { x.ScriptId, x.Language });
                    table.ForeignKey(
                        name: "FK_ScriptsTranslations_Scripts_ScriptId",
                        column: x => x.ScriptId,
                        principalSchema: "Scripts",
                        principalTable: "Scripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScriptTags",
                schema: "Scripts",
                columns: table => new
                {
                    ScriptId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScriptTags", x => new { x.ScriptId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ScriptTags_Scripts_ScriptId",
                        column: x => x.ScriptId,
                        principalSchema: "Scripts",
                        principalTable: "Scripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScriptTags_Tags_TagId",
                        column: x => x.TagId,
                        principalSchema: "Scripts",
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestDatas",
                schema: "Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScriptId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestDatas_Scripts_ScriptId",
                        column: x => x.ScriptId,
                        principalSchema: "Scripts",
                        principalTable: "Scripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Views",
                schema: "Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScriptId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ViewDefinition = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Views", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Views_Scripts_ScriptId",
                        column: x => x.ScriptId,
                        principalSchema: "Scripts",
                        principalTable: "Scripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitsTranslations",
                schema: "Scripts",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitsTranslations", x => new { x.UnitId, x.Language });
                    table.ForeignKey(
                        name: "FK_UnitsTranslations_Units_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "Scripts",
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeadLoadLayers",
                schema: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    DeadLoadId = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: true),
                    Height = table.Column<double>(type: "float", nullable: true),
                    Length = table.Column<double>(type: "float", nullable: true),
                    PreviousDeadLoadId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeadLoadLayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeadLoadLayers_DeadLoadLayers_PreviousDeadLoadId",
                        column: x => x.PreviousDeadLoadId,
                        principalSchema: "Projects",
                        principalTable: "DeadLoadLayers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeadLoadLayers_DeadLoads_DeadLoadId",
                        column: x => x.DeadLoadId,
                        principalSchema: "Projects",
                        principalTable: "DeadLoads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeadLoadLayers_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalSchema: "DeadLoads",
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialAdditions",
                schema: "DeadLoads",
                columns: table => new
                {
                    AdditionId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialAdditions", x => new { x.MaterialId, x.AdditionId });
                    table.ForeignKey(
                        name: "FK_MaterialAdditions_Additions_AdditionId",
                        column: x => x.AdditionId,
                        principalSchema: "DeadLoads",
                        principalTable: "Additions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialAdditions_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalSchema: "DeadLoads",
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParameterGroupTranslations",
                schema: "Scripts",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterGroupTranslations", x => new { x.GroupId, x.Language });
                    table.ForeignKey(
                        name: "FK_ParameterGroupTranslations_ParameterGroups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Scripts",
                        principalTable: "ParameterGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                schema: "Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ValueType = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false, defaultValue: "Number"),
                    Value = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    VisibilityValidator = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    DataValidator = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ValueOptionSetting = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "None"),
                    ParameterOptions = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "None"),
                    ParameterGroupId = table.Column<int>(type: "int", nullable: true),
                    AccordingTo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ScriptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parameters_ParameterGroups_ParameterGroupId",
                        column: x => x.ParameterGroupId,
                        principalSchema: "Scripts",
                        principalTable: "ParameterGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Parameters_Scripts_ScriptId",
                        column: x => x.ScriptId,
                        principalSchema: "Scripts",
                        principalTable: "Scripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScriptGroupsTranslations",
                schema: "Scripts",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ScriptGroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScriptGroupsTranslations", x => new { x.GroupId, x.Language });
                    table.ForeignKey(
                        name: "FK_ScriptGroupsTranslations_ParameterGroups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Scripts",
                        principalTable: "ParameterGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScriptGroupsTranslations_ScriptGroups_ScriptGroupId",
                        column: x => x.ScriptGroupId,
                        principalSchema: "Scripts",
                        principalTable: "ScriptGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Assertions",
                schema: "Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assertions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assertions_TestDatas_TestDataId",
                        column: x => x.TestDataId,
                        principalSchema: "Scripts",
                        principalTable: "TestDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParameterFigures",
                schema: "Scripts",
                columns: table => new
                {
                    ParameterId = table.Column<int>(type: "int", nullable: false),
                    FigureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterFigures", x => new { x.ParameterId, x.FigureId });
                    table.ForeignKey(
                        name: "FK_ParameterFigures_Figures_FigureId",
                        column: x => x.FigureId,
                        principalSchema: "Scripts",
                        principalTable: "Figures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParameterFigures_Parameters_ParameterId",
                        column: x => x.ParameterId,
                        principalSchema: "Scripts",
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParameterInputs",
                schema: "Projects",
                columns: table => new
                {
                    ScriptDatatId = table.Column<int>(type: "int", nullable: false),
                    ParameterId = table.Column<int>(type: "int", nullable: false),
                    ScriptDataId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterInputs", x => new { x.ParameterId, x.ScriptDatatId });
                    table.ForeignKey(
                        name: "FK_ParameterInputs_Parameters_ParameterId",
                        column: x => x.ParameterId,
                        principalSchema: "Scripts",
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParameterInputs_ScriptDatas_ScriptDataId",
                        column: x => x.ScriptDataId,
                        principalSchema: "Projects",
                        principalTable: "ScriptDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ParametersTranslations",
                schema: "Scripts",
                columns: table => new
                {
                    ParameterId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametersTranslations", x => new { x.ParameterId, x.Language });
                    table.ForeignKey(
                        name: "FK_ParametersTranslations_Parameters_ParameterId",
                        column: x => x.ParameterId,
                        principalSchema: "Scripts",
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParameterUnits",
                schema: "Scripts",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    ParameterId = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<double>(type: "float", nullable: false, defaultValue: 1.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterUnits", x => new { x.ParameterId, x.UnitId });
                    table.ForeignKey(
                        name: "FK_ParameterUnits_Parameters_ParameterId",
                        column: x => x.ParameterId,
                        principalSchema: "Scripts",
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParameterUnits_Units_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "Scripts",
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValueOptions",
                schema: "Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParameterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValueOptions_Parameters_ParameterId",
                        column: x => x.ParameterId,
                        principalSchema: "Scripts",
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParameterInputUnits",
                schema: "Projects",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    ParameterInputId = table.Column<int>(type: "int", nullable: false),
                    ParameterInputParameterId = table.Column<int>(type: "int", nullable: true),
                    ParameterInputScriptDatatId = table.Column<int>(type: "int", nullable: true),
                    Power = table.Column<double>(type: "float", nullable: false, defaultValue: 1.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterInputUnits", x => new { x.ParameterInputId, x.UnitId });
                    table.ForeignKey(
                        name: "FK_ParameterInputUnits_ParameterInputs_ParameterInputParameterId_ParameterInputScriptDatatId",
                        columns: x => new { x.ParameterInputParameterId, x.ParameterInputScriptDatatId },
                        principalSchema: "Projects",
                        principalTable: "ParameterInputs",
                        principalColumns: new[] { "ParameterId", "ScriptDatatId" });
                    table.ForeignKey(
                        name: "FK_ParameterInputUnits_Units_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "Scripts",
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestParameters",
                schema: "Scripts",
                columns: table => new
                {
                    ParameterUnitId = table.Column<int>(type: "int", nullable: false),
                    TestDataId = table.Column<int>(type: "int", nullable: false),
                    ParameterUnitParameterId = table.Column<int>(type: "int", nullable: false),
                    ParameterUnitUnitId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestParameters", x => new { x.ParameterUnitId, x.TestDataId });
                    table.ForeignKey(
                        name: "FK_TestParameters_ParameterUnits_ParameterUnitParameterId_ParameterUnitUnitId",
                        columns: x => new { x.ParameterUnitParameterId, x.ParameterUnitUnitId },
                        principalSchema: "Scripts",
                        principalTable: "ParameterUnits",
                        principalColumns: new[] { "ParameterId", "UnitId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestParameters_TestDatas_TestDataId",
                        column: x => x.TestDataId,
                        principalSchema: "Scripts",
                        principalTable: "TestDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ValueOptionFigures",
                schema: "Scripts",
                columns: table => new
                {
                    ValueOptionId = table.Column<int>(type: "int", nullable: false),
                    FigureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueOptionFigures", x => new { x.ValueOptionId, x.FigureId });
                    table.ForeignKey(
                        name: "FK_ValueOptionFigures_Figures_FigureId",
                        column: x => x.FigureId,
                        principalSchema: "Scripts",
                        principalTable: "Figures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ValueOptionFigures_ValueOptions_ValueOptionId",
                        column: x => x.ValueOptionId,
                        principalSchema: "Scripts",
                        principalTable: "ValueOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValueOptionsTranslations",
                schema: "Scripts",
                columns: table => new
                {
                    ValueOptionId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueOptionsTranslations", x => new { x.ValueOptionId, x.Language });
                    table.ForeignKey(
                        name: "FK_ValueOptionsTranslations_ValueOptions_ValueOptionId",
                        column: x => x.ValueOptionId,
                        principalSchema: "Scripts",
                        principalTable: "ValueOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Application_Tokens_ApplicationUserId",
                table: "Application_Tokens",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Assertions_TestDataId",
                schema: "Scripts",
                table: "Assertions",
                column: "TestDataId");

            migrationBuilder.CreateIndex(
                name: "IX_DeadLoadLayers_DeadLoadId",
                schema: "Projects",
                table: "DeadLoadLayers",
                column: "DeadLoadId");

            migrationBuilder.CreateIndex(
                name: "IX_DeadLoadLayers_MaterialId",
                schema: "Projects",
                table: "DeadLoadLayers",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_DeadLoadLayers_PreviousDeadLoadId",
                schema: "Projects",
                table: "DeadLoadLayers",
                column: "PreviousDeadLoadId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_Keys_Use",
                table: "Keys",
                column: "Use");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialAdditions_AdditionId",
                schema: "DeadLoads",
                table: "MaterialAdditions",
                column: "AdditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_SubcategoryId",
                schema: "DeadLoads",
                table: "Materials",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterFigures_FigureId",
                schema: "Scripts",
                table: "ParameterFigures",
                column: "FigureId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterGroups_ScriptId",
                schema: "Scripts",
                table: "ParameterGroups",
                column: "ScriptId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterInputs_ScriptDataId",
                schema: "Projects",
                table: "ParameterInputs",
                column: "ScriptDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterInputUnits_ParameterInputParameterId_ParameterInputScriptDatatId",
                schema: "Projects",
                table: "ParameterInputUnits",
                columns: new[] { "ParameterInputParameterId", "ParameterInputScriptDatatId" });

            migrationBuilder.CreateIndex(
                name: "IX_ParameterInputUnits_UnitId",
                schema: "Projects",
                table: "ParameterInputUnits",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_ProfileTypeId",
                schema: "Profiles",
                table: "Parameters",
                column: "ProfileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_ParameterGroupId",
                schema: "Scripts",
                table: "Parameters",
                column: "ParameterGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_ScriptId",
                schema: "Scripts",
                table: "Parameters",
                column: "ScriptId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterUnits_UnitId",
                schema: "Scripts",
                table: "ParameterUnits",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterValues_ParameterId",
                schema: "Profiles",
                table: "ParameterValues",
                column: "ParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterValues_SteelProfileId",
                schema: "Profiles",
                table: "ParameterValues",
                column: "SteelProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_ConsumedTime",
                table: "PersistedGrants",
                column: "ConsumedTime");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDeadLoads_DeadLoadId",
                schema: "Projects",
                table: "ProjectDeadLoads",
                column: "DeadLoadId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDesignerClaims_ClaimId",
                schema: "Projects",
                table: "ProjectDesignerClaims",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDesignerClaims_DesignerUserId",
                schema: "Projects",
                table: "ProjectDesignerClaims",
                column: "DesignerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDesignerClaims_ProjectId",
                schema: "Projects",
                table: "ProjectDesignerClaims",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CompanyId",
                schema: "Projects",
                table: "Projects",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScripts_ProjectId",
                schema: "Projects",
                table: "ProjectScripts",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ScriptFigures_FigureId",
                schema: "Scripts",
                table: "ScriptFigures",
                column: "FigureId");

            migrationBuilder.CreateIndex(
                name: "IX_ScriptGroupsTranslations_ScriptGroupId",
                schema: "Scripts",
                table: "ScriptGroupsTranslations",
                column: "ScriptGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Scripts_GroupId",
                schema: "Scripts",
                table: "Scripts",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Scripts_PreviousScriptId",
                schema: "Scripts",
                table: "Scripts",
                column: "PreviousScriptId");

            migrationBuilder.CreateIndex(
                name: "IX_ScriptTags_TagId",
                schema: "Scripts",
                table: "ScriptTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionPoints_ProfileTypeId",
                schema: "Profiles",
                table: "SectionPoints",
                column: "ProfileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SteelProfiles_ProfileTypeId",
                schema: "Profiles",
                table: "SteelProfiles",
                column: "ProfileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CategoryId",
                schema: "DeadLoads",
                table: "Subcategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TestDatas_ScriptId",
                schema: "Scripts",
                table: "TestDatas",
                column: "ScriptId");

            migrationBuilder.CreateIndex(
                name: "IX_TestParameters_ParameterUnitParameterId_ParameterUnitUnitId",
                schema: "Scripts",
                table: "TestParameters",
                columns: new[] { "ParameterUnitParameterId", "ParameterUnitUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_TestParameters_TestDataId",
                schema: "Scripts",
                table: "TestParameters",
                column: "TestDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_UnitGroupId",
                schema: "Scripts",
                table: "Units",
                column: "UnitGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueOptionFigures_FigureId",
                schema: "Scripts",
                table: "ValueOptionFigures",
                column: "FigureId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueOptions_ParameterId",
                schema: "Scripts",
                table: "ValueOptions",
                column: "ParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_Views_ScriptId",
                schema: "Scripts",
                table: "Views",
                column: "ScriptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Application_Tokens");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Assertions",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "DeadLoadLayers",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "Keys");

            migrationBuilder.DropTable(
                name: "MaterialAdditions",
                schema: "DeadLoads");

            migrationBuilder.DropTable(
                name: "ParameterFigures",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "ParameterGroupTranslations",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "ParameterInputUnits",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "ParametersTranslations",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "ParameterValues",
                schema: "Profiles");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "ProjectDeadLoads",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "ProjectDesignerClaims",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "ProjectScripts",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "ScriptFigures",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "ScriptGroupsTranslations",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "ScriptsTranslations",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "ScriptTags",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "SectionPoints",
                schema: "Profiles");

            migrationBuilder.DropTable(
                name: "TestParameters",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "UnitGroupTranslations",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "UnitsTranslations",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "ValueOptionFigures",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "ValueOptionsTranslations",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "Views",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "Application_Users");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Additions",
                schema: "DeadLoads");

            migrationBuilder.DropTable(
                name: "Materials",
                schema: "DeadLoads");

            migrationBuilder.DropTable(
                name: "ParameterInputs",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "Parameters",
                schema: "Profiles");

            migrationBuilder.DropTable(
                name: "SteelProfiles",
                schema: "Profiles");

            migrationBuilder.DropTable(
                name: "DeadLoads",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "Claims",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "Designers",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "Tags",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "ParameterUnits",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "TestDatas",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "Figures",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "ValueOptions",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "Subcategories",
                schema: "DeadLoads");

            migrationBuilder.DropTable(
                name: "ScriptDatas",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "ProfileTypes",
                schema: "Profiles");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "Units",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "Parameters",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "DeadLoads");

            migrationBuilder.DropTable(
                name: "UnitGroups",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "ParameterGroups",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "Scripts",
                schema: "Scripts");

            migrationBuilder.DropTable(
                name: "ScriptGroups",
                schema: "Scripts");
        }
    }
}
