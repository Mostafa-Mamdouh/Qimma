using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "BasCities",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CityAbbrCode = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasCities", x => new { x.CountryId, x.CityId });
                });

            migrationBuilder.CreateTable(
                name: "BasCountries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryNameAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CountryNameEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    NationalityNameFrn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    NationalityNameNtv = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CountryCodeISO = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasCountries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerProfile",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerNameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RefCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ActiveFlag = table.Column<bool>(type: "bit", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProfile", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "InternalRoles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleNameAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    RoleNameEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    RoleCode = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalRoles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "InternalScreens",
                columns: table => new
                {
                    ScreenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenNameAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ScreenNameEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ScreenCode = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalScreens", x => x.ScreenId);
                });

            migrationBuilder.CreateTable(
                name: "ItemHierarchyStructure",
                columns: table => new
                {
                    ItemStructureCode = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    ItemStructureDesFrn = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    ItemStructureDesNtv = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    StructureGeneralDetailFlag = table.Column<bool>(type: "bit", nullable: false),
                    StructureLevelNum = table.Column<int>(type: "int", nullable: false),
                    ParentStructure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultIssueAccountCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemHierarchyStructure", x => x.ItemStructureCode);
                });

            migrationBuilder.CreateTable(
                name: "ItemSourceStatus",
                columns: table => new
                {
                    ItemSourceStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSourceStatus", x => x.ItemSourceStatusId);
                });

            migrationBuilder.CreateTable(
                name: "LegalRepresentativesProfile",
                columns: table => new
                {
                    LegalRepresentativesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegalRepresentativesNameAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    LegalRepresentativesNameEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    NationalID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmailId = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CurrentFacilities = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    AmanInsertedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalRepresentativesProfile", x => x.LegalRepresentativesId);
                });

            migrationBuilder.CreateTable(
                name: "ListOfValues",
                columns: table => new
                {
                    LovId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LovNameAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    LovNameEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    LovCode = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    SqlStatement = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListOfValues", x => x.LovId);
                });

            migrationBuilder.CreateTable(
                name: "LookupSet",
                columns: table => new
                {
                    LookupSetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    DisplayNameAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    DisplayNameEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ExtraData1 = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ExtraData2 = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupSet", x => x.LookupSetId);
                });

            migrationBuilder.CreateTable(
                name: "RelatedItemHierarchyStructure",
                columns: table => new
                {
                    ItemStructureCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ItemStructureDesFrn = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ItemStructureDesNtv = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StructureLevelNum = table.Column<int>(type: "int", nullable: false),
                    StructureGeneralDetailFlag = table.Column<int>(type: "int", nullable: false),
                    ParentStructure = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ItemStructureLongDes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedItemHierarchyStructure", x => x.ItemStructureCode);
                });

            migrationBuilder.CreateTable(
                name: "SafetyResponsibleOfficersProfile",
                columns: table => new
                {
                    SROId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SRONameAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    SRONameEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    NationalID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmailId = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    CertificateNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IssuanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmanInsertedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyResponsibleOfficersProfile", x => x.SROId);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypeMaster",
                columns: table => new
                {
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionTypeDesFrn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionTypeDesNtv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypeMaster", x => x.TransactionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TreeControl",
                columns: table => new
                {
                    TreeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LevelNum = table.Column<int>(type: "int", nullable: false),
                    LevelInterval = table.Column<int>(type: "int", nullable: true),
                    LevelLength = table.Column<int>(type: "int", nullable: true),
                    LevelPadding = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LevelTitleFRN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LevelTitleNTV = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StartingNum = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeControl", x => new { x.TreeCode, x.LevelNum });
                });

            migrationBuilder.CreateTable(
                name: "WorkersDosimeters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LineNum = table.Column<int>(type: "int", nullable: false),
                    DosimeterType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DosimeterID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: true),
                    StartWearDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndWearDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkersDosimeters", x => new { x.Id, x.LineNum });
                });

            migrationBuilder.CreateTable(
                name: "WorkersExposuresDoses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LineNum = table.Column<int>(type: "int", nullable: false),
                    MeasurementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DosimeterType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DosimeterID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeepDose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeepDoseUOM = table.Column<int>(type: "int", nullable: true),
                    DeepTotalAccumulatedDose1Yr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeepTotalAccumulatedDose5Yr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShallowDose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShallowDoseUOM = table.Column<int>(type: "int", nullable: true),
                    ShallowTotalAccumulatedDose1Yr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EyeDose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EyeDoseUOM = table.Column<int>(type: "int", nullable: true),
                    EyeTotalAccumulatedDose1Yr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EyeTotalAccumulatedDose5Yr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NeutronDose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NeutronDoseUOM = table.Column<int>(type: "int", nullable: true),
                    InitialDayofMonitoring = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastDayofMonitoring = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkersExposuresDoses", x => new { x.Id, x.LineNum });
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
                name: "AspNetUsers",
                columns: table => new
                {
                    FirstNameAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    SecondNameAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    LastNameNameAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    FirstNameEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    SecondNameEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    LastNameNameEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    IsActiveUser = table.Column<bool>(type: "bit", nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    PictureContentType = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    PictureName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    PassportNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GregorianBirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HijriBirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NationalID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IqamaID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    UserAlternatePhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Nationality = table.Column<int>(type: "int", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CountryID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_AspNetUsers_BasCountries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "BasCountries",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateTable(
                name: "ManufacturerMaster",
                columns: table => new
                {
                    ManufacturerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerDescAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ManufacturerDescEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmailId = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<int>(type: "int", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    POBox = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturerMaster", x => x.ManufacturerId);
                    table.ForeignKey(
                        name: "FK_ManufacturerMaster_BasCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "BasCountries",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateTable(
                name: "InternalScreenRoles",
                columns: table => new
                {
                    ScreenRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenOrder = table.Column<int>(type: "int", nullable: false),
                    Insert = table.Column<bool>(type: "bit", nullable: false),
                    Update = table.Column<bool>(type: "bit", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: false),
                    Query = table.Column<bool>(type: "bit", nullable: false),
                    ScreenId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalScreenRoles", x => x.ScreenRoleId);
                    table.ForeignKey(
                        name: "FK_InternalScreenRoles_InternalRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "InternalRoles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternalScreenRoles_InternalScreens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "InternalScreens",
                        principalColumn: "ScreenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceItemProfile",
                columns: table => new
                {
                    ServiceItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemDesFrn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ItemDesNtv = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ItemStructureCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ItemStructureCode1 = table.Column<string>(type: "nvarchar(120)", nullable: true),
                    CurrentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IssueAccountCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemRefCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    ItmQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MultiStage = table.Column<bool>(type: "bit", nullable: false),
                    LicenseTerm = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceItemProfile", x => x.ServiceItemId);
                    table.ForeignKey(
                        name: "FK_ServiceItemProfile_ItemHierarchyStructure_ItemStructureCode1",
                        column: x => x.ItemStructureCode1,
                        principalTable: "ItemHierarchyStructure",
                        principalColumn: "ItemStructureCode");
                });

            migrationBuilder.CreateTable(
                name: "InternalScreenFields",
                columns: table => new
                {
                    FieldId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldDescAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    FieldDescEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    FieldType = table.Column<int>(type: "int", nullable: false),
                    FieldFormat = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ScreenId = table.Column<int>(type: "int", nullable: false),
                    LookupSetId = table.Column<int>(type: "int", nullable: true),
                    LovId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalScreenFields", x => x.FieldId);
                    table.ForeignKey(
                        name: "FK_InternalScreenFields_InternalScreens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "InternalScreens",
                        principalColumn: "ScreenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternalScreenFields_ListOfValues_LovId",
                        column: x => x.LovId,
                        principalTable: "ListOfValues",
                        principalColumn: "LovId");
                    table.ForeignKey(
                        name: "FK_InternalScreenFields_LookupSet_LookupSetId",
                        column: x => x.LookupSetId,
                        principalTable: "LookupSet",
                        principalColumn: "LookupSetId");
                });

            migrationBuilder.CreateTable(
                name: "LookupSetTerm",
                columns: table => new
                {
                    LookupSetTermId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    DisplayNameAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    DisplayNameEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ExtraData1 = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ExtraData2 = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LookupSetId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupSetTerm", x => x.LookupSetTermId);
                    table.ForeignKey(
                        name: "FK_LookupSetTerm_LookupSet",
                        column: x => x.LookupSetId,
                        principalTable: "LookupSet",
                        principalColumn: "LookupSetId");
                });

            migrationBuilder.CreateTable(
                name: "TransactionHeader",
                columns: table => new
                {
                    TrnHeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfirmedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrnStatus = table.Column<int>(type: "int", nullable: false),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHeader", x => x.TrnHeaderId);
                    table.ForeignKey(
                        name: "FK_TransactionHeader_TransactionTypeMaster_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypeMaster",
                        principalColumn: "TransactionTypeId",
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "ServiceItemPrice",
                columns: table => new
                {
                    ServiceItemPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceItemId = table.Column<int>(type: "int", nullable: false),
                    LineNum = table.Column<int>(type: "int", nullable: false),
                    ItemPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceItemPrice", x => x.ServiceItemPriceId);
                    table.ForeignKey(
                        name: "FK_ServiceItemPrice_ServiceItemProfile_ServiceItemId",
                        column: x => x.ServiceItemId,
                        principalTable: "ServiceItemProfile",
                        principalColumn: "ServiceItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternalFieldPermissions",
                columns: table => new
                {
                    FieldPermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeId = table.Column<int>(type: "int", nullable: false),
                    FieldId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalFieldPermissions", x => x.FieldPermissionId);
                    table.ForeignKey(
                        name: "FK_InternalFieldPermissions_InternalScreenFields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "InternalScreenFields",
                        principalColumn: "FieldId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillingServiceTrnHeader",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransactionRefNum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    StatusFlag = table.Column<int>(type: "int", nullable: true),
                    TrnRemarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ExRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TermsCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    InvoiceSource = table.Column<int>(type: "int", nullable: true),
                    InvoiceBU = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingServiceTrnHeader", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_BillingServiceTrnHeader_ItemHierarchyStructure_InvoiceBU",
                        column: x => x.InvoiceBU,
                        principalTable: "ItemHierarchyStructure",
                        principalColumn: "ItemStructureCode");
                    table.ForeignKey(
                        name: "FK_BillingServiceTrnHeader_LookupSetTerm_StatusFlag",
                        column: x => x.StatusFlag,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                });

            migrationBuilder.CreateTable(
                name: "EntityProfile",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityNameAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    EntityNameEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmailId = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    GovernmentID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EntityType = table.Column<int>(type: "int", maxLength: 20, nullable: true),
                    AmanInsertedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityProfile", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_EntityProfile_LookupSetTerm_EntityType",
                        column: x => x.EntityType,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                });

            migrationBuilder.CreateTable(
                name: "Radionuclides",
                columns: table => new
                {
                    RadionuclideId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Isotop = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    HalfLife = table.Column<float>(type: "real", nullable: true),
                    HalfLifeUnitId = table.Column<int>(type: "int", nullable: true),
                    ExemptionValue = table.Column<float>(type: "real", nullable: true),
                    ExemptionValueUnitId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radionuclides", x => x.RadionuclideId);
                    table.ForeignKey(
                        name: "FK_Radionuclides_LookupSetTerm_ExemptionValueUnitId",
                        column: x => x.ExemptionValueUnitId,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_Radionuclides_LookupSetTerm_HalfLifeUnitId",
                        column: x => x.HalfLifeUnitId,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                });

            migrationBuilder.CreateTable(
                name: "RelatedItems",
                columns: table => new
                {
                    RelatedItemCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ManufacturerSerialNom = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    FacilityRelatedItemID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    PermitinitialQty = table.Column<double>(type: "float", nullable: true),
                    DateOfManufacturing = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ItemTypeNumber = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ItemTypeName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    HSCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    EndUserCertificate = table.Column<bool>(type: "bit", nullable: true),
                    GovernmentCommitments = table.Column<bool>(type: "bit", nullable: true),
                    Attachments = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    StructureLevelNum = table.Column<int>(type: "int", maxLength: 450, nullable: true),
                    NuclearEntityId = table.Column<int>(type: "int", nullable: true),
                    GeneralDetailFlag = table.Column<bool>(type: "bit", nullable: true),
                    ItemStructureCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: true),
                    ItemCategory = table.Column<int>(type: "int", nullable: true),
                    Manufacturer = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedItems", x => x.RelatedItemCode);
                    table.ForeignKey(
                        name: "FK_RelatedItems_LookupSetTerm_ItemCategory",
                        column: x => x.ItemCategory,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_RelatedItems_LookupSetTerm_StatusID",
                        column: x => x.StatusID,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_RelatedItems_ManufacturerMaster_Manufacturer",
                        column: x => x.Manufacturer,
                        principalTable: "ManufacturerMaster",
                        principalColumn: "ManufacturerId");
                });

            migrationBuilder.CreateTable(
                name: "BillingServiceTrnDetails",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false),
                    LineNum = table.Column<int>(type: "int", nullable: false),
                    ServiceItemId = table.Column<int>(type: "int", nullable: true),
                    ItemQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LineRemarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VatIncluded = table.Column<bool>(type: "bit", nullable: false),
                    VatPcntg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BillableQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingServiceTrnDetails", x => new { x.LineNum, x.TransactionID });
                    table.ForeignKey(
                        name: "FK_BillingServiceTrnDetails_BillingServiceTrnHeader_TransactionID",
                        column: x => x.TransactionID,
                        principalTable: "BillingServiceTrnHeader",
                        principalColumn: "TransactionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacilityProfile",
                columns: table => new
                {
                    FacilityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityNameAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    FacilityNameEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    NationalAddress = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    FacilityCode = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Province = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    City = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    AmanInsertedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityProfile", x => x.FacilityId);
                    table.ForeignKey(
                        name: "FK_FacilityProfile_EntityProfile_EntityId",
                        column: x => x.EntityId,
                        principalTable: "EntityProfile",
                        principalColumn: "EntityId");
                });

            migrationBuilder.CreateTable(
                name: "RelatedItemFiles",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelatedItemFileId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RealtedItemCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FileNum = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    FileOriginalName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    UploadType = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    FileBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Attachments = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedItemFiles", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_RelatedItemFiles_RelatedItems_Attachments",
                        column: x => x.Attachments,
                        principalTable: "RelatedItems",
                        principalColumn: "RelatedItemCode");
                    table.ForeignKey(
                        name: "FK_RelatedItemFiles_RelatedItems_RealtedItemCode",
                        column: x => x.RealtedItemCode,
                        principalTable: "RelatedItems",
                        principalColumn: "RelatedItemCode");
                });

            migrationBuilder.CreateTable(
                name: "LicenseProfile",
                columns: table => new
                {
                    LicenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseDescAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    LicenseDescEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    LicenseCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LicenseVersionNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PractiseSector = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LicenseActivities = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    FacilityId = table.Column<int>(type: "int", nullable: true),
                    AmanInsertedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseProfile", x => x.LicenseId);
                    table.ForeignKey(
                        name: "FK_LicenseProfile_EntityProfile_EntityId",
                        column: x => x.EntityId,
                        principalTable: "EntityProfile",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_LicenseProfile_FacilityProfile_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "FacilityProfile",
                        principalColumn: "FacilityId");
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerNameAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    WorkerNameEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", maxLength: 200, nullable: false),
                    JobPosition = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    FacilityId = table.Column<int>(type: "int", nullable: true),
                    Nationality = table.Column<int>(type: "int", nullable: true),
                    NationalityId = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Status = table.Column<int>(type: "int", maxLength: 80, nullable: true),
                    PassportNo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    CurrentDosimeterType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CurrentDosimeterID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workers_BasCountries_Nationality",
                        column: x => x.Nationality,
                        principalTable: "BasCountries",
                        principalColumn: "CountryId");
                    table.ForeignKey(
                        name: "FK_Workers_FacilityProfile_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "FacilityProfile",
                        principalColumn: "FacilityId");
                    table.ForeignKey(
                        name: "FK_Workers_LookupSetTerm_Status",
                        column: x => x.Status,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                });

            migrationBuilder.CreateTable(
                name: "LicenseInventoryLimits",
                columns: table => new
                {
                    LicenseInventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceType = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Radionuclide = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    MaximumRadioactivity = table.Column<int>(type: "int", nullable: false),
                    NumberofSources = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: true),
                    AmanInsertedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseInventoryLimits", x => x.LicenseInventoryId);
                    table.ForeignKey(
                        name: "FK_LicenseInventoryLimits_LicenseProfile_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "LicenseProfile",
                        principalColumn: "LicenseId");
                });

            migrationBuilder.CreateTable(
                name: "PermitDetailsProfile",
                columns: table => new
                {
                    PermitDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermitDetailsDescAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    PermitDetailsDescEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    PermitNumber = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: true),
                    AmanInsertedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitDetailsProfile", x => x.PermitDetailsId);
                    table.ForeignKey(
                        name: "FK_PermitDetailsProfile_LicenseProfile_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "LicenseProfile",
                        principalColumn: "LicenseId");
                });

            migrationBuilder.CreateTable(
                name: "PermitInventoryLimits",
                columns: table => new
                {
                    PermitInventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceSerialNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ManufactureName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Radionuclide = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ModelMaximumRadioactivity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PermitDetailsId = table.Column<int>(type: "int", nullable: true),
                    AmanInsertedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitInventoryLimits", x => x.PermitInventoryId);
                    table.ForeignKey(
                        name: "FK_PermitInventoryLimits_PermitDetailsProfile_PermitDetailsId",
                        column: x => x.PermitDetailsId,
                        principalTable: "PermitDetailsProfile",
                        principalColumn: "PermitDetailsId");
                });

            migrationBuilder.CreateTable(
                name: "PractiseProfile",
                columns: table => new
                {
                    PractiseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PractiseNameAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    PractiseNameEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    PermitDetailsId = table.Column<int>(type: "int", nullable: true),
                    AmanInsertedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmmanId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PractiseProfile", x => x.PractiseId);
                    table.ForeignKey(
                        name: "FK_PractiseProfile_PermitDetailsProfile_PermitDetailsId",
                        column: x => x.PermitDetailsId,
                        principalTable: "PermitDetailsProfile",
                        principalColumn: "PermitDetailsId");
                });

            migrationBuilder.CreateTable(
                name: "NuclearRelatedItemsProfile",
                columns: table => new
                {
                    NRRCRelatedItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelatedItemDescAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    RelatedItemDescEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    NrrcID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ManufacturerSerialNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DateofManufacturing = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityRelatedItemID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ItemTypeNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ItemtypeName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ModelNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    HSCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    GovernmentCommitmentsFlag = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EndUserCertificateFlag = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PermitInitialQty = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    FacilityId = table.Column<int>(type: "int", nullable: true),
                    LicenseId = table.Column<int>(type: "int", nullable: true),
                    LicenseInventoryId = table.Column<int>(type: "int", nullable: true),
                    PermitdetailsId = table.Column<int>(type: "int", nullable: true),
                    PermitInventoryId = table.Column<int>(type: "int", nullable: true),
                    PractiseId = table.Column<int>(type: "int", nullable: true),
                    SROId = table.Column<int>(type: "int", nullable: true),
                    LegalRepresentativesId = table.Column<int>(type: "int", nullable: true),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    ManufactureCountryId = table.Column<int>(type: "int", nullable: true),
                    ItemCategory = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NuclearRelatedItemsProfile", x => x.NRRCRelatedItemId);
                    table.ForeignKey(
                        name: "FK_NuclearRelatedItemsProfile_BasCountries_ManufactureCountryId",
                        column: x => x.ManufactureCountryId,
                        principalTable: "BasCountries",
                        principalColumn: "CountryId");
                    table.ForeignKey(
                        name: "FK_NuclearRelatedItemsProfile_EntityProfile_EntityId",
                        column: x => x.EntityId,
                        principalTable: "EntityProfile",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_NuclearRelatedItemsProfile_FacilityProfile_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "FacilityProfile",
                        principalColumn: "FacilityId");
                    table.ForeignKey(
                        name: "FK_NuclearRelatedItemsProfile_LegalRepresentativesProfile_LegalRepresentativesId",
                        column: x => x.LegalRepresentativesId,
                        principalTable: "LegalRepresentativesProfile",
                        principalColumn: "LegalRepresentativesId");
                    table.ForeignKey(
                        name: "FK_NuclearRelatedItemsProfile_LicenseInventoryLimits_LicenseInventoryId",
                        column: x => x.LicenseInventoryId,
                        principalTable: "LicenseInventoryLimits",
                        principalColumn: "LicenseInventoryId");
                    table.ForeignKey(
                        name: "FK_NuclearRelatedItemsProfile_LicenseProfile_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "LicenseProfile",
                        principalColumn: "LicenseId");
                    table.ForeignKey(
                        name: "FK_NuclearRelatedItemsProfile_LookupSet_ItemCategory",
                        column: x => x.ItemCategory,
                        principalTable: "LookupSet",
                        principalColumn: "LookupSetId");
                    table.ForeignKey(
                        name: "FK_NuclearRelatedItemsProfile_ManufacturerMaster_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "ManufacturerMaster",
                        principalColumn: "ManufacturerId");
                    table.ForeignKey(
                        name: "FK_NuclearRelatedItemsProfile_PermitDetailsProfile_PermitdetailsId",
                        column: x => x.PermitdetailsId,
                        principalTable: "PermitDetailsProfile",
                        principalColumn: "PermitDetailsId");
                    table.ForeignKey(
                        name: "FK_NuclearRelatedItemsProfile_PermitInventoryLimits_PermitInventoryId",
                        column: x => x.PermitInventoryId,
                        principalTable: "PermitInventoryLimits",
                        principalColumn: "PermitInventoryId");
                    table.ForeignKey(
                        name: "FK_NuclearRelatedItemsProfile_PractiseProfile_PractiseId",
                        column: x => x.PractiseId,
                        principalTable: "PractiseProfile",
                        principalColumn: "PractiseId");
                    table.ForeignKey(
                        name: "FK_NuclearRelatedItemsProfile_SafetyResponsibleOfficersProfile_SROId",
                        column: x => x.SROId,
                        principalTable: "SafetyResponsibleOfficersProfile",
                        principalColumn: "SROId");
                });

            migrationBuilder.CreateTable(
                name: "RadiationGeneratorsProfile",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentDescAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    EquipmentDescEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    NrrcID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ManufacturerSerialNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DateofManufacturing = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilitySerialNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ModelNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    XRayTubeSerialNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MaxEnergy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EnergyUnit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MaxDoseRate = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DoseUnit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MaxCurrent = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SheildMaterial = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ShieldNuclearMaterialCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    FacilityId = table.Column<int>(type: "int", nullable: true),
                    LicenseId = table.Column<int>(type: "int", nullable: true),
                    LicenseInventoryId = table.Column<int>(type: "int", nullable: true),
                    PermitdetailsId = table.Column<int>(type: "int", nullable: true),
                    PermitInventoryId = table.Column<int>(type: "int", nullable: true),
                    PractiseId = table.Column<int>(type: "int", nullable: true),
                    SROId = table.Column<int>(type: "int", nullable: true),
                    LegalRepresentativesId = table.Column<int>(type: "int", nullable: true),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    EquipmentType = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadiationGeneratorsProfile", x => x.EquipmentId);
                    table.ForeignKey(
                        name: "FK_RadiationGeneratorsProfile_EntityProfile_EntityId",
                        column: x => x.EntityId,
                        principalTable: "EntityProfile",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_RadiationGeneratorsProfile_FacilityProfile_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "FacilityProfile",
                        principalColumn: "FacilityId");
                    table.ForeignKey(
                        name: "FK_RadiationGeneratorsProfile_LegalRepresentativesProfile_LegalRepresentativesId",
                        column: x => x.LegalRepresentativesId,
                        principalTable: "LegalRepresentativesProfile",
                        principalColumn: "LegalRepresentativesId");
                    table.ForeignKey(
                        name: "FK_RadiationGeneratorsProfile_LicenseInventoryLimits_LicenseInventoryId",
                        column: x => x.LicenseInventoryId,
                        principalTable: "LicenseInventoryLimits",
                        principalColumn: "LicenseInventoryId");
                    table.ForeignKey(
                        name: "FK_RadiationGeneratorsProfile_LicenseProfile_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "LicenseProfile",
                        principalColumn: "LicenseId");
                    table.ForeignKey(
                        name: "FK_RadiationGeneratorsProfile_LookupSet_EquipmentType",
                        column: x => x.EquipmentType,
                        principalTable: "LookupSet",
                        principalColumn: "LookupSetId");
                    table.ForeignKey(
                        name: "FK_RadiationGeneratorsProfile_ManufacturerMaster_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "ManufacturerMaster",
                        principalColumn: "ManufacturerId");
                    table.ForeignKey(
                        name: "FK_RadiationGeneratorsProfile_PermitDetailsProfile_PermitdetailsId",
                        column: x => x.PermitdetailsId,
                        principalTable: "PermitDetailsProfile",
                        principalColumn: "PermitDetailsId");
                    table.ForeignKey(
                        name: "FK_RadiationGeneratorsProfile_PermitInventoryLimits_PermitInventoryId",
                        column: x => x.PermitInventoryId,
                        principalTable: "PermitInventoryLimits",
                        principalColumn: "PermitInventoryId");
                    table.ForeignKey(
                        name: "FK_RadiationGeneratorsProfile_PractiseProfile_PractiseId",
                        column: x => x.PractiseId,
                        principalTable: "PractiseProfile",
                        principalColumn: "PractiseId");
                    table.ForeignKey(
                        name: "FK_RadiationGeneratorsProfile_SafetyResponsibleOfficersProfile_SROId",
                        column: x => x.SROId,
                        principalTable: "SafetyResponsibleOfficersProfile",
                        principalColumn: "SROId");
                });

            migrationBuilder.CreateTable(
                name: "TrnItemSource",
                columns: table => new
                {
                    TrnSourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NrrcId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturerSerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilitySerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitialActivityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    ShieldNuclearMaterialCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfAcquiredSources = table.Column<int>(type: "int", nullable: true),
                    NumberOfConsumedSources = table.Column<int>(type: "int", nullable: true),
                    InitialMass = table.Column<double>(type: "float", nullable: true),
                    EnrichmentOfUranium = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceActivity = table.Column<double>(type: "float", nullable: true),
                    IsShieldDU = table.Column<bool>(type: "bit", nullable: false),
                    ShieldPhysicalForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShieldChemicalForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoCertificate = table.Column<bool>(type: "bit", nullable: true),
                    SourceType = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    PhysicalForm = table.Column<int>(type: "int", nullable: true),
                    AssociatedEquipment = table.Column<int>(type: "int", nullable: true),
                    NuclearMaterial = table.Column<int>(type: "int", nullable: true),
                    InitialMassUnit = table.Column<int>(type: "int", nullable: true),
                    SourceCategory = table.Column<int>(type: "int", nullable: true),
                    TransactionHeaderId = table.Column<int>(type: "int", nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    FacilityId = table.Column<int>(type: "int", nullable: true),
                    LicenseId = table.Column<int>(type: "int", nullable: true),
                    LicenseInventoryId = table.Column<int>(type: "int", nullable: true),
                    PermitdetailsId = table.Column<int>(type: "int", nullable: true),
                    PermitInventoryId = table.Column<int>(type: "int", nullable: true),
                    PractiseId = table.Column<int>(type: "int", nullable: true),
                    SROId = table.Column<int>(type: "int", nullable: true),
                    LegalRepresentativesId = table.Column<int>(type: "int", nullable: true),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    ManufacturerCountryId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrnItemSource", x => x.TrnSourceId);
                    table.ForeignKey(
                        name: "FK_TrnItemSource_EntityProfile_EntityId",
                        column: x => x.EntityId,
                        principalTable: "EntityProfile",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_FacilityProfile_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "FacilityProfile",
                        principalColumn: "FacilityId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_LegalRepresentativesProfile_LegalRepresentativesId",
                        column: x => x.LegalRepresentativesId,
                        principalTable: "LegalRepresentativesProfile",
                        principalColumn: "LegalRepresentativesId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_LicenseInventoryLimits_LicenseInventoryId",
                        column: x => x.LicenseInventoryId,
                        principalTable: "LicenseInventoryLimits",
                        principalColumn: "LicenseInventoryId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_LicenseProfile_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "LicenseProfile",
                        principalColumn: "LicenseId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_LookupSetTerm_AssociatedEquipment",
                        column: x => x.AssociatedEquipment,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_LookupSetTerm_InitialMassUnit",
                        column: x => x.InitialMassUnit,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_LookupSetTerm_ManufacturerCountryId",
                        column: x => x.ManufacturerCountryId,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_LookupSetTerm_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_LookupSetTerm_NuclearMaterial",
                        column: x => x.NuclearMaterial,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_LookupSetTerm_PhysicalForm",
                        column: x => x.PhysicalForm,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_LookupSetTerm_SourceCategory",
                        column: x => x.SourceCategory,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_LookupSetTerm_SourceType",
                        column: x => x.SourceType,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_LookupSetTerm_Status",
                        column: x => x.Status,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_PermitDetailsProfile_PermitdetailsId",
                        column: x => x.PermitdetailsId,
                        principalTable: "PermitDetailsProfile",
                        principalColumn: "PermitDetailsId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_PermitInventoryLimits_PermitInventoryId",
                        column: x => x.PermitInventoryId,
                        principalTable: "PermitInventoryLimits",
                        principalColumn: "PermitInventoryId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_PractiseProfile_PractiseId",
                        column: x => x.PractiseId,
                        principalTable: "PractiseProfile",
                        principalColumn: "PractiseId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_SafetyResponsibleOfficersProfile_SROId",
                        column: x => x.SROId,
                        principalTable: "SafetyResponsibleOfficersProfile",
                        principalColumn: "SROId");
                    table.ForeignKey(
                        name: "FK_TrnItemSource_TransactionHeader_TransactionHeaderId",
                        column: x => x.TransactionHeaderId,
                        principalTable: "TransactionHeader",
                        principalColumn: "TrnHeaderId");
                });

            migrationBuilder.CreateTable(
                name: "TrnItemFiles",
                columns: table => new
                {
                    TrnItemSourceFileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileNum = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileOriginalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    AttachmentType = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForShield = table.Column<bool>(type: "bit", nullable: true),
                    TrnSourceID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrnItemFiles", x => x.TrnItemSourceFileId);
                    table.ForeignKey(
                        name: "FK_TrnItemFiles_TrnItemSource_TrnSourceID",
                        column: x => x.TrnSourceID,
                        principalTable: "TrnItemSource",
                        principalColumn: "TrnSourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrnItemSourceRadionuclides",
                columns: table => new
                {
                    TrnRadionuclideId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InitialActivity = table.Column<float>(type: "real", nullable: true),
                    InitialActivityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InitialActivityUnit = table.Column<int>(type: "int", nullable: true),
                    RadionulcideId = table.Column<int>(type: "int", nullable: true),
                    TrnSourceID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrnItemSourceRadionuclides", x => x.TrnRadionuclideId);
                    table.ForeignKey(
                        name: "FK_TrnItemSourceRadionuclides_LookupSetTerm_InitialActivityUnit",
                        column: x => x.InitialActivityUnit,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_TrnItemSourceRadionuclides_Radionuclides_RadionulcideId",
                        column: x => x.RadionulcideId,
                        principalTable: "Radionuclides",
                        principalColumn: "RadionuclideId");
                    table.ForeignKey(
                        name: "FK_TrnItemSourceRadionuclides_TrnItemSource_TrnSourceID",
                        column: x => x.TrnSourceID,
                        principalTable: "TrnItemSource",
                        principalColumn: "TrnSourceId");
                });

            migrationBuilder.CreateTable(
                name: "ItemSourceFiles",
                columns: table => new
                {
                    ItemSourceFileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileNum = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileOriginalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    AttachmentType = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForShield = table.Column<bool>(type: "bit", nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSourceFiles", x => x.ItemSourceFileId);
                });

            migrationBuilder.CreateTable(
                name: "ItemSourceMsgHistory",
                columns: table => new
                {
                    MsgId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MsgText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSourceMsgHistory", x => x.MsgId);
                });

            migrationBuilder.CreateTable(
                name: "ItemSourceProfile",
                columns: table => new
                {
                    SourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NrrcId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturerSerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilitySerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitialActivityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    ShieldNuclearMaterialCode = table.Column<int>(type: "int", nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfAcquiredSources = table.Column<int>(type: "int", nullable: true),
                    NumberOfConsumedSources = table.Column<int>(type: "int", nullable: true),
                    InitialMass = table.Column<double>(type: "float", nullable: true),
                    EnrichmentOfUranium = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceActivity = table.Column<double>(type: "float", nullable: true),
                    IsShieldDU = table.Column<bool>(type: "bit", nullable: true),
                    ShieldPhysicalForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShieldChemicalForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentTrnStatus = table.Column<int>(type: "int", nullable: false),
                    SourceModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoCertificate = table.Column<bool>(type: "bit", nullable: false),
                    SourceType = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    PhysicalForm = table.Column<int>(type: "int", nullable: true),
                    AssociatedEquipment = table.Column<int>(type: "int", nullable: true),
                    NuclearMaterial = table.Column<int>(type: "int", nullable: true),
                    InitialMassUnit = table.Column<int>(type: "int", nullable: true),
                    SourceCategory = table.Column<int>(type: "int", nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    FacilityId = table.Column<int>(type: "int", nullable: true),
                    LicenseId = table.Column<int>(type: "int", nullable: true),
                    LicenseInventoryId = table.Column<int>(type: "int", nullable: true),
                    PermitdetailsId = table.Column<int>(type: "int", nullable: true),
                    PermitInventoryId = table.Column<int>(type: "int", nullable: true),
                    PractiseId = table.Column<int>(type: "int", nullable: true),
                    SROId = table.Column<int>(type: "int", nullable: true),
                    LegalRepresentativesId = table.Column<int>(type: "int", nullable: true),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    ManufacturerCountryId = table.Column<int>(type: "int", nullable: true),
                    TrnSourceId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSourceProfile", x => x.SourceId);
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_EntityProfile_EntityId",
                        column: x => x.EntityId,
                        principalTable: "EntityProfile",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_FacilityProfile_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "FacilityProfile",
                        principalColumn: "FacilityId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_LegalRepresentativesProfile_LegalRepresentativesId",
                        column: x => x.LegalRepresentativesId,
                        principalTable: "LegalRepresentativesProfile",
                        principalColumn: "LegalRepresentativesId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_LicenseInventoryLimits_LicenseInventoryId",
                        column: x => x.LicenseInventoryId,
                        principalTable: "LicenseInventoryLimits",
                        principalColumn: "LicenseInventoryId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_LicenseProfile_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "LicenseProfile",
                        principalColumn: "LicenseId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_LookupSetTerm_AssociatedEquipment",
                        column: x => x.AssociatedEquipment,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_LookupSetTerm_InitialMassUnit",
                        column: x => x.InitialMassUnit,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_LookupSetTerm_ManufacturerCountryId",
                        column: x => x.ManufacturerCountryId,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_LookupSetTerm_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_LookupSetTerm_NuclearMaterial",
                        column: x => x.NuclearMaterial,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_LookupSetTerm_PhysicalForm",
                        column: x => x.PhysicalForm,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_LookupSetTerm_SourceCategory",
                        column: x => x.SourceCategory,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_LookupSetTerm_SourceType",
                        column: x => x.SourceType,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_LookupSetTerm_Status",
                        column: x => x.Status,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_PermitDetailsProfile_PermitdetailsId",
                        column: x => x.PermitdetailsId,
                        principalTable: "PermitDetailsProfile",
                        principalColumn: "PermitDetailsId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_PermitInventoryLimits_PermitInventoryId",
                        column: x => x.PermitInventoryId,
                        principalTable: "PermitInventoryLimits",
                        principalColumn: "PermitInventoryId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_PractiseProfile_PractiseId",
                        column: x => x.PractiseId,
                        principalTable: "PractiseProfile",
                        principalColumn: "PractiseId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_SafetyResponsibleOfficersProfile_SROId",
                        column: x => x.SROId,
                        principalTable: "SafetyResponsibleOfficersProfile",
                        principalColumn: "SROId");
                    table.ForeignKey(
                        name: "FK_ItemSourceProfile_TrnItemSource_TrnSourceId",
                        column: x => x.TrnSourceId,
                        principalTable: "TrnItemSource",
                        principalColumn: "TrnSourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemSourceRadionulcides",
                columns: table => new
                {
                    ItemSourceRadionuclideId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InitialActivity = table.Column<float>(type: "real", nullable: false),
                    InitialActivityUnit = table.Column<int>(type: "int", nullable: true),
                    RadionulcideId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSourceRadionulcides", x => x.ItemSourceRadionuclideId);
                    table.ForeignKey(
                        name: "FK_ItemSourceRadionulcides_ItemSourceProfile_SourceId",
                        column: x => x.SourceId,
                        principalTable: "ItemSourceProfile",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSourceRadionulcides_LookupSetTerm_InitialActivityUnit",
                        column: x => x.InitialActivityUnit,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_ItemSourceRadionulcides_Radionuclides_RadionulcideId",
                        column: x => x.RadionulcideId,
                        principalTable: "Radionuclides",
                        principalColumn: "RadionuclideId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemSourceStatusHistory",
                columns: table => new
                {
                    StatusHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    ParentStatusId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSourceStatusHistory", x => x.StatusHistoryId);
                    table.ForeignKey(
                        name: "FK_ItemSourceStatusHistory_ItemSourceProfile_SourceId",
                        column: x => x.SourceId,
                        principalTable: "ItemSourceProfile",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSourceStatusHistory_ItemSourceStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ItemSourceStatus",
                        principalColumn: "ItemSourceStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSourceStatusHistory_ItemSourceStatusHistory_ParentStatusId",
                        column: x => x.ParentStatusId,
                        principalTable: "ItemSourceStatusHistory",
                        principalColumn: "StatusHistoryId");
                });

            migrationBuilder.CreateTable(
                name: "NuclearMaterials",
                columns: table => new
                {
                    SourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NrrcId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturerSerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilitySerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitialActivityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitialMass = table.Column<double>(type: "float", nullable: true),
                    ChemicalForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsShield = table.Column<bool>(type: "bit", nullable: true),
                    SourceModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    PhysicalForm = table.Column<int>(type: "int", nullable: true),
                    NuclearMaterialType = table.Column<int>(type: "int", nullable: true),
                    InitialMassUnit = table.Column<int>(type: "int", nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    FacilityId = table.Column<int>(type: "int", nullable: true),
                    LicenseId = table.Column<int>(type: "int", nullable: true),
                    LicenseInventoryId = table.Column<int>(type: "int", nullable: true),
                    PermitdetailsId = table.Column<int>(type: "int", nullable: true),
                    PermitInventoryId = table.Column<int>(type: "int", nullable: true),
                    PractiseId = table.Column<int>(type: "int", nullable: true),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    ManufacturerCountryId = table.Column<int>(type: "int", nullable: true),
                    ItemSourceProfileId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NuclearMaterials", x => x.SourceId);
                    table.ForeignKey(
                        name: "FK_NuclearMaterials_EntityProfile_EntityId",
                        column: x => x.EntityId,
                        principalTable: "EntityProfile",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_NuclearMaterials_FacilityProfile_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "FacilityProfile",
                        principalColumn: "FacilityId");
                    table.ForeignKey(
                        name: "FK_NuclearMaterials_ItemSourceProfile_ItemSourceProfileId",
                        column: x => x.ItemSourceProfileId,
                        principalTable: "ItemSourceProfile",
                        principalColumn: "SourceId");
                    table.ForeignKey(
                        name: "FK_NuclearMaterials_LicenseInventoryLimits_LicenseInventoryId",
                        column: x => x.LicenseInventoryId,
                        principalTable: "LicenseInventoryLimits",
                        principalColumn: "LicenseInventoryId");
                    table.ForeignKey(
                        name: "FK_NuclearMaterials_LicenseProfile_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "LicenseProfile",
                        principalColumn: "LicenseId");
                    table.ForeignKey(
                        name: "FK_NuclearMaterials_LookupSetTerm_InitialMassUnit",
                        column: x => x.InitialMassUnit,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_NuclearMaterials_LookupSetTerm_ManufacturerCountryId",
                        column: x => x.ManufacturerCountryId,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_NuclearMaterials_LookupSetTerm_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_NuclearMaterials_LookupSetTerm_NuclearMaterialType",
                        column: x => x.NuclearMaterialType,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_NuclearMaterials_LookupSetTerm_PhysicalForm",
                        column: x => x.PhysicalForm,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_NuclearMaterials_LookupSetTerm_Status",
                        column: x => x.Status,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_NuclearMaterials_PermitDetailsProfile_PermitdetailsId",
                        column: x => x.PermitdetailsId,
                        principalTable: "PermitDetailsProfile",
                        principalColumn: "PermitDetailsId");
                    table.ForeignKey(
                        name: "FK_NuclearMaterials_PermitInventoryLimits_PermitInventoryId",
                        column: x => x.PermitInventoryId,
                        principalTable: "PermitInventoryLimits",
                        principalColumn: "PermitInventoryId");
                    table.ForeignKey(
                        name: "FK_NuclearMaterials_PractiseProfile_PractiseId",
                        column: x => x.PractiseId,
                        principalTable: "PractiseProfile",
                        principalColumn: "PractiseId");
                });

            migrationBuilder.CreateTable(
                name: "TrnItemSourceHistory",
                columns: table => new
                {
                    TrnHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransactionAttribute = table.Column<int>(type: "int", nullable: false),
                    ItemSourceProfileId = table.Column<int>(type: "int", nullable: true),
                    TransactionId = table.Column<int>(type: "int", nullable: true),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrnItemSourceHistory", x => x.TrnHistoryId);
                    table.ForeignKey(
                        name: "FK_TrnItemSourceHistory_ItemSourceProfile_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "ItemSourceProfile",
                        principalColumn: "SourceId");
                    table.ForeignKey(
                        name: "FK_TrnItemSourceHistory_TransactionTypeMaster_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypeMaster",
                        principalColumn: "TransactionTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NuclearMaterialFiles",
                columns: table => new
                {
                    ItemSourceFileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileNum = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileOriginalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    AttachmentType = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NuclearMaterialId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NuclearMaterialFiles", x => x.ItemSourceFileId);
                    table.ForeignKey(
                        name: "FK_NuclearMaterialFiles_NuclearMaterials_NuclearMaterialId",
                        column: x => x.NuclearMaterialId,
                        principalTable: "NuclearMaterials",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NuclearMaterialRadionulcides",
                columns: table => new
                {
                    NuclearMaterialRadionulcideId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enrichment = table.Column<float>(type: "real", nullable: true),
                    RadionulcideId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NuclearMaterialRadionulcides", x => x.NuclearMaterialRadionulcideId);
                    table.ForeignKey(
                        name: "FK_NuclearMaterialRadionulcides_NuclearMaterials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "NuclearMaterials",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NuclearMaterialRadionulcides_Radionuclides_RadionulcideId",
                        column: x => x.RadionulcideId,
                        principalTable: "Radionuclides",
                        principalColumn: "RadionuclideId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_AspNetUsers_CountryID",
                table: "AspNetUsers",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BillingServiceTrnDetails_TransactionID",
                table: "BillingServiceTrnDetails",
                column: "TransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_BillingServiceTrnHeader_InvoiceBU",
                table: "BillingServiceTrnHeader",
                column: "InvoiceBU");

            migrationBuilder.CreateIndex(
                name: "IX_BillingServiceTrnHeader_StatusFlag",
                table: "BillingServiceTrnHeader",
                column: "StatusFlag");

            migrationBuilder.CreateIndex(
                name: "IX_EntityProfile_EntityType",
                table: "EntityProfile",
                column: "EntityType");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityProfile_EntityId",
                table: "FacilityProfile",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFieldPermissions_FieldId",
                table: "InternalFieldPermissions",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalScreenFields_LookupSetId",
                table: "InternalScreenFields",
                column: "LookupSetId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalScreenFields_LovId",
                table: "InternalScreenFields",
                column: "LovId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalScreenFields_ScreenId",
                table: "InternalScreenFields",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalScreenRoles_RoleId",
                table: "InternalScreenRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalScreenRoles_ScreenId",
                table: "InternalScreenRoles",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceFiles_SourceId",
                table: "ItemSourceFiles",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceMsgHistory_SourceId",
                table: "ItemSourceMsgHistory",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_AssociatedEquipment",
                table: "ItemSourceProfile",
                column: "AssociatedEquipment");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_EntityId",
                table: "ItemSourceProfile",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_FacilityId",
                table: "ItemSourceProfile",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_InitialMassUnit",
                table: "ItemSourceProfile",
                column: "InitialMassUnit");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_LegalRepresentativesId",
                table: "ItemSourceProfile",
                column: "LegalRepresentativesId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_LicenseId",
                table: "ItemSourceProfile",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_LicenseInventoryId",
                table: "ItemSourceProfile",
                column: "LicenseInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_ManufacturerCountryId",
                table: "ItemSourceProfile",
                column: "ManufacturerCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_ManufacturerId",
                table: "ItemSourceProfile",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_NuclearMaterial",
                table: "ItemSourceProfile",
                column: "NuclearMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_PermitdetailsId",
                table: "ItemSourceProfile",
                column: "PermitdetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_PermitInventoryId",
                table: "ItemSourceProfile",
                column: "PermitInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_PhysicalForm",
                table: "ItemSourceProfile",
                column: "PhysicalForm");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_PractiseId",
                table: "ItemSourceProfile",
                column: "PractiseId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_ShieldNuclearMaterialCode",
                table: "ItemSourceProfile",
                column: "ShieldNuclearMaterialCode",
                unique: true,
                filter: "[ShieldNuclearMaterialCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_SourceCategory",
                table: "ItemSourceProfile",
                column: "SourceCategory");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_SourceType",
                table: "ItemSourceProfile",
                column: "SourceType");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_SROId",
                table: "ItemSourceProfile",
                column: "SROId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_Status",
                table: "ItemSourceProfile",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_TrnSourceId",
                table: "ItemSourceProfile",
                column: "TrnSourceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceRadionulcides_InitialActivityUnit",
                table: "ItemSourceRadionulcides",
                column: "InitialActivityUnit");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceRadionulcides_RadionulcideId",
                table: "ItemSourceRadionulcides",
                column: "RadionulcideId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceRadionulcides_SourceId",
                table: "ItemSourceRadionulcides",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceStatusHistory_ParentStatusId",
                table: "ItemSourceStatusHistory",
                column: "ParentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceStatusHistory_SourceId",
                table: "ItemSourceStatusHistory",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceStatusHistory_StatusId",
                table: "ItemSourceStatusHistory",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseInventoryLimits_LicenseId",
                table: "LicenseInventoryLimits",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseProfile_EntityId",
                table: "LicenseProfile",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseProfile_FacilityId",
                table: "LicenseProfile",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_LookupSetTerm_LookupSetId",
                table: "LookupSetTerm",
                column: "LookupSetId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturerMaster_CountryId",
                table: "ManufacturerMaster",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterialFiles_NuclearMaterialId",
                table: "NuclearMaterialFiles",
                column: "NuclearMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterialRadionulcides_MaterialId",
                table: "NuclearMaterialRadionulcides",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterialRadionulcides_RadionulcideId",
                table: "NuclearMaterialRadionulcides",
                column: "RadionulcideId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_EntityId",
                table: "NuclearMaterials",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_FacilityId",
                table: "NuclearMaterials",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_InitialMassUnit",
                table: "NuclearMaterials",
                column: "InitialMassUnit");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_ItemSourceProfileId",
                table: "NuclearMaterials",
                column: "ItemSourceProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_LicenseId",
                table: "NuclearMaterials",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_LicenseInventoryId",
                table: "NuclearMaterials",
                column: "LicenseInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_ManufacturerCountryId",
                table: "NuclearMaterials",
                column: "ManufacturerCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_ManufacturerId",
                table: "NuclearMaterials",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_NuclearMaterialType",
                table: "NuclearMaterials",
                column: "NuclearMaterialType");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_PermitdetailsId",
                table: "NuclearMaterials",
                column: "PermitdetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_PermitInventoryId",
                table: "NuclearMaterials",
                column: "PermitInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_PhysicalForm",
                table: "NuclearMaterials",
                column: "PhysicalForm");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_PractiseId",
                table: "NuclearMaterials",
                column: "PractiseId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_Status",
                table: "NuclearMaterials",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearRelatedItemsProfile_EntityId",
                table: "NuclearRelatedItemsProfile",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearRelatedItemsProfile_FacilityId",
                table: "NuclearRelatedItemsProfile",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearRelatedItemsProfile_ItemCategory",
                table: "NuclearRelatedItemsProfile",
                column: "ItemCategory");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearRelatedItemsProfile_LegalRepresentativesId",
                table: "NuclearRelatedItemsProfile",
                column: "LegalRepresentativesId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearRelatedItemsProfile_LicenseId",
                table: "NuclearRelatedItemsProfile",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearRelatedItemsProfile_LicenseInventoryId",
                table: "NuclearRelatedItemsProfile",
                column: "LicenseInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearRelatedItemsProfile_ManufactureCountryId",
                table: "NuclearRelatedItemsProfile",
                column: "ManufactureCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearRelatedItemsProfile_ManufacturerId",
                table: "NuclearRelatedItemsProfile",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearRelatedItemsProfile_PermitdetailsId",
                table: "NuclearRelatedItemsProfile",
                column: "PermitdetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearRelatedItemsProfile_PermitInventoryId",
                table: "NuclearRelatedItemsProfile",
                column: "PermitInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearRelatedItemsProfile_PractiseId",
                table: "NuclearRelatedItemsProfile",
                column: "PractiseId");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearRelatedItemsProfile_SROId",
                table: "NuclearRelatedItemsProfile",
                column: "SROId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitDetailsProfile_LicenseId",
                table: "PermitDetailsProfile",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitInventoryLimits_PermitDetailsId",
                table: "PermitInventoryLimits",
                column: "PermitDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_PractiseProfile_PermitDetailsId",
                table: "PractiseProfile",
                column: "PermitDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_RadiationGeneratorsProfile_EntityId",
                table: "RadiationGeneratorsProfile",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RadiationGeneratorsProfile_EquipmentType",
                table: "RadiationGeneratorsProfile",
                column: "EquipmentType");

            migrationBuilder.CreateIndex(
                name: "IX_RadiationGeneratorsProfile_FacilityId",
                table: "RadiationGeneratorsProfile",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_RadiationGeneratorsProfile_LegalRepresentativesId",
                table: "RadiationGeneratorsProfile",
                column: "LegalRepresentativesId");

            migrationBuilder.CreateIndex(
                name: "IX_RadiationGeneratorsProfile_LicenseId",
                table: "RadiationGeneratorsProfile",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_RadiationGeneratorsProfile_LicenseInventoryId",
                table: "RadiationGeneratorsProfile",
                column: "LicenseInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RadiationGeneratorsProfile_ManufacturerId",
                table: "RadiationGeneratorsProfile",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_RadiationGeneratorsProfile_PermitdetailsId",
                table: "RadiationGeneratorsProfile",
                column: "PermitdetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_RadiationGeneratorsProfile_PermitInventoryId",
                table: "RadiationGeneratorsProfile",
                column: "PermitInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RadiationGeneratorsProfile_PractiseId",
                table: "RadiationGeneratorsProfile",
                column: "PractiseId");

            migrationBuilder.CreateIndex(
                name: "IX_RadiationGeneratorsProfile_SROId",
                table: "RadiationGeneratorsProfile",
                column: "SROId");

            migrationBuilder.CreateIndex(
                name: "IX_Radionuclides_ExemptionValueUnitId",
                table: "Radionuclides",
                column: "ExemptionValueUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Radionuclides_HalfLifeUnitId",
                table: "Radionuclides",
                column: "HalfLifeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItemFiles_Attachments",
                table: "RelatedItemFiles",
                column: "Attachments");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItemFiles_RealtedItemCode",
                table: "RelatedItemFiles",
                column: "RealtedItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItems_ItemCategory",
                table: "RelatedItems",
                column: "ItemCategory");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItems_Manufacturer",
                table: "RelatedItems",
                column: "Manufacturer");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItems_StatusID",
                table: "RelatedItems",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItemPrice_ServiceItemId",
                table: "ServiceItemPrice",
                column: "ServiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItemProfile_ItemStructureCode1",
                table: "ServiceItemProfile",
                column: "ItemStructureCode1");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHeader_TransactionTypeId",
                table: "TransactionHeader",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemFiles_TrnSourceID",
                table: "TrnItemFiles",
                column: "TrnSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_AssociatedEquipment",
                table: "TrnItemSource",
                column: "AssociatedEquipment");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_EntityId",
                table: "TrnItemSource",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_FacilityId",
                table: "TrnItemSource",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_InitialMassUnit",
                table: "TrnItemSource",
                column: "InitialMassUnit");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_LegalRepresentativesId",
                table: "TrnItemSource",
                column: "LegalRepresentativesId");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_LicenseId",
                table: "TrnItemSource",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_LicenseInventoryId",
                table: "TrnItemSource",
                column: "LicenseInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_ManufacturerCountryId",
                table: "TrnItemSource",
                column: "ManufacturerCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_ManufacturerId",
                table: "TrnItemSource",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_NuclearMaterial",
                table: "TrnItemSource",
                column: "NuclearMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_PermitdetailsId",
                table: "TrnItemSource",
                column: "PermitdetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_PermitInventoryId",
                table: "TrnItemSource",
                column: "PermitInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_PhysicalForm",
                table: "TrnItemSource",
                column: "PhysicalForm");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_PractiseId",
                table: "TrnItemSource",
                column: "PractiseId");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_SourceCategory",
                table: "TrnItemSource",
                column: "SourceCategory");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_SourceType",
                table: "TrnItemSource",
                column: "SourceType");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_SROId",
                table: "TrnItemSource",
                column: "SROId");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_Status",
                table: "TrnItemSource",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_TransactionHeaderId",
                table: "TrnItemSource",
                column: "TransactionHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSourceHistory_TransactionId",
                table: "TrnItemSourceHistory",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSourceHistory_TransactionTypeId",
                table: "TrnItemSourceHistory",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSourceRadionuclides_InitialActivityUnit",
                table: "TrnItemSourceRadionuclides",
                column: "InitialActivityUnit");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSourceRadionuclides_RadionulcideId",
                table: "TrnItemSourceRadionuclides",
                column: "RadionulcideId");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSourceRadionuclides_TrnSourceID",
                table: "TrnItemSourceRadionuclides",
                column: "TrnSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_FacilityId",
                table: "Workers",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_Nationality",
                table: "Workers",
                column: "Nationality");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_Status",
                table: "Workers",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSourceFiles_ItemSourceProfile_SourceId",
                table: "ItemSourceFiles",
                column: "SourceId",
                principalTable: "ItemSourceProfile",
                principalColumn: "SourceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSourceMsgHistory_ItemSourceProfile_SourceId",
                table: "ItemSourceMsgHistory",
                column: "SourceId",
                principalTable: "ItemSourceProfile",
                principalColumn: "SourceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSourceProfile_NuclearMaterials_ShieldNuclearMaterialCode",
                table: "ItemSourceProfile",
                column: "ShieldNuclearMaterialCode",
                principalTable: "NuclearMaterials",
                principalColumn: "SourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityProfile_LookupSetTerm_EntityType",
                table: "EntityProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSourceProfile_LookupSetTerm_AssociatedEquipment",
                table: "ItemSourceProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSourceProfile_LookupSetTerm_InitialMassUnit",
                table: "ItemSourceProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSourceProfile_LookupSetTerm_ManufacturerCountryId",
                table: "ItemSourceProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSourceProfile_LookupSetTerm_ManufacturerId",
                table: "ItemSourceProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSourceProfile_LookupSetTerm_NuclearMaterial",
                table: "ItemSourceProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSourceProfile_LookupSetTerm_PhysicalForm",
                table: "ItemSourceProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSourceProfile_LookupSetTerm_SourceCategory",
                table: "ItemSourceProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSourceProfile_LookupSetTerm_SourceType",
                table: "ItemSourceProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSourceProfile_LookupSetTerm_Status",
                table: "ItemSourceProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_NuclearMaterials_LookupSetTerm_InitialMassUnit",
                table: "NuclearMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_NuclearMaterials_LookupSetTerm_ManufacturerCountryId",
                table: "NuclearMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_NuclearMaterials_LookupSetTerm_ManufacturerId",
                table: "NuclearMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_NuclearMaterials_LookupSetTerm_NuclearMaterialType",
                table: "NuclearMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_NuclearMaterials_LookupSetTerm_PhysicalForm",
                table: "NuclearMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_NuclearMaterials_LookupSetTerm_Status",
                table: "NuclearMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_TrnItemSource_LookupSetTerm_AssociatedEquipment",
                table: "TrnItemSource");

            migrationBuilder.DropForeignKey(
                name: "FK_TrnItemSource_LookupSetTerm_InitialMassUnit",
                table: "TrnItemSource");

            migrationBuilder.DropForeignKey(
                name: "FK_TrnItemSource_LookupSetTerm_ManufacturerCountryId",
                table: "TrnItemSource");

            migrationBuilder.DropForeignKey(
                name: "FK_TrnItemSource_LookupSetTerm_ManufacturerId",
                table: "TrnItemSource");

            migrationBuilder.DropForeignKey(
                name: "FK_TrnItemSource_LookupSetTerm_NuclearMaterial",
                table: "TrnItemSource");

            migrationBuilder.DropForeignKey(
                name: "FK_TrnItemSource_LookupSetTerm_PhysicalForm",
                table: "TrnItemSource");

            migrationBuilder.DropForeignKey(
                name: "FK_TrnItemSource_LookupSetTerm_SourceCategory",
                table: "TrnItemSource");

            migrationBuilder.DropForeignKey(
                name: "FK_TrnItemSource_LookupSetTerm_SourceType",
                table: "TrnItemSource");

            migrationBuilder.DropForeignKey(
                name: "FK_TrnItemSource_LookupSetTerm_Status",
                table: "TrnItemSource");

            migrationBuilder.DropForeignKey(
                name: "FK_FacilityProfile_EntityProfile_EntityId",
                table: "FacilityProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSourceProfile_EntityProfile_EntityId",
                table: "ItemSourceProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_LicenseProfile_EntityProfile_EntityId",
                table: "LicenseProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_NuclearMaterials_EntityProfile_EntityId",
                table: "NuclearMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_TrnItemSource_EntityProfile_EntityId",
                table: "TrnItemSource");

            migrationBuilder.DropForeignKey(
                name: "FK_NuclearMaterials_ItemSourceProfile_ItemSourceProfileId",
                table: "NuclearMaterials");

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
                name: "BasCities");

            migrationBuilder.DropTable(
                name: "BillingServiceTrnDetails");

            migrationBuilder.DropTable(
                name: "CustomerProfile");

            migrationBuilder.DropTable(
                name: "InternalFieldPermissions");

            migrationBuilder.DropTable(
                name: "InternalScreenRoles");

            migrationBuilder.DropTable(
                name: "ItemSourceFiles");

            migrationBuilder.DropTable(
                name: "ItemSourceMsgHistory");

            migrationBuilder.DropTable(
                name: "ItemSourceRadionulcides");

            migrationBuilder.DropTable(
                name: "ItemSourceStatusHistory");

            migrationBuilder.DropTable(
                name: "NuclearMaterialFiles");

            migrationBuilder.DropTable(
                name: "NuclearMaterialRadionulcides");

            migrationBuilder.DropTable(
                name: "NuclearRelatedItemsProfile");

            migrationBuilder.DropTable(
                name: "RadiationGeneratorsProfile");

            migrationBuilder.DropTable(
                name: "RelatedItemFiles");

            migrationBuilder.DropTable(
                name: "RelatedItemHierarchyStructure");

            migrationBuilder.DropTable(
                name: "ServiceItemPrice");

            migrationBuilder.DropTable(
                name: "TreeControl");

            migrationBuilder.DropTable(
                name: "TrnItemFiles");

            migrationBuilder.DropTable(
                name: "TrnItemSourceHistory");

            migrationBuilder.DropTable(
                name: "TrnItemSourceRadionuclides");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "WorkersDosimeters");

            migrationBuilder.DropTable(
                name: "WorkersExposuresDoses");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BillingServiceTrnHeader");

            migrationBuilder.DropTable(
                name: "InternalScreenFields");

            migrationBuilder.DropTable(
                name: "InternalRoles");

            migrationBuilder.DropTable(
                name: "ItemSourceStatus");

            migrationBuilder.DropTable(
                name: "RelatedItems");

            migrationBuilder.DropTable(
                name: "ServiceItemProfile");

            migrationBuilder.DropTable(
                name: "Radionuclides");

            migrationBuilder.DropTable(
                name: "InternalScreens");

            migrationBuilder.DropTable(
                name: "ListOfValues");

            migrationBuilder.DropTable(
                name: "ManufacturerMaster");

            migrationBuilder.DropTable(
                name: "ItemHierarchyStructure");

            migrationBuilder.DropTable(
                name: "BasCountries");

            migrationBuilder.DropTable(
                name: "LookupSetTerm");

            migrationBuilder.DropTable(
                name: "LookupSet");

            migrationBuilder.DropTable(
                name: "EntityProfile");

            migrationBuilder.DropTable(
                name: "ItemSourceProfile");

            migrationBuilder.DropTable(
                name: "NuclearMaterials");

            migrationBuilder.DropTable(
                name: "TrnItemSource");

            migrationBuilder.DropTable(
                name: "LegalRepresentativesProfile");

            migrationBuilder.DropTable(
                name: "LicenseInventoryLimits");

            migrationBuilder.DropTable(
                name: "PermitInventoryLimits");

            migrationBuilder.DropTable(
                name: "PractiseProfile");

            migrationBuilder.DropTable(
                name: "SafetyResponsibleOfficersProfile");

            migrationBuilder.DropTable(
                name: "TransactionHeader");

            migrationBuilder.DropTable(
                name: "PermitDetailsProfile");

            migrationBuilder.DropTable(
                name: "TransactionTypeMaster");

            migrationBuilder.DropTable(
                name: "LicenseProfile");

            migrationBuilder.DropTable(
                name: "FacilityProfile");
        }
    }
}
