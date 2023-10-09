using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class practise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LicenseInventoryLimits_LicenseProfile_LicenseId",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropForeignKey(
                name: "FK_PermitInventoryLimits_PermitDetailsProfile_PermitDetailsId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropIndex(
                name: "IX_PermitInventoryLimits_PermitDetailsId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropIndex(
                name: "IX_LicenseInventoryLimits_LicenseId",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropColumn(
                name: "AmanInsertedOn",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "ManufactureName",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "ModelMaximumRadioactivity",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "PermitDetailsId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "Radionuclide",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "SourceSerialNo",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "AmanInsertedOn",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropColumn(
                name: "Radionuclide",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropColumn(
                name: "SourceType",
                table: "LicenseInventoryLimits");

            migrationBuilder.RenameColumn(
                name: "NumberofSources",
                table: "LicenseInventoryLimits",
                newName: "LicenseMasterId");

            migrationBuilder.RenameColumn(
                name: "MaximumRadioactivity",
                table: "LicenseInventoryLimits",
                newName: "FacilityId");

            migrationBuilder.AddColumn<int>(
                name: "FacilityId",
                table: "PermitInventoryLimits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LicenseMasterId",
                table: "PermitInventoryLimits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PermitMasterId",
                table: "PermitInventoryLimits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PractiseSector",
                table: "LicenseProfile",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LicensePractices",
                table: "LicenseProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Province",
                table: "FacilityProfile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "FacilityProfile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "LicenseGenerators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseInventoryId = table.Column<int>(type: "int", nullable: false),
                    MaximumVoltage = table.Column<double>(type: "float", nullable: false),
                    MaximumTubeCurrent = table.Column<double>(type: "float", nullable: false),
                    MaximumEnergy = table.Column<double>(type: "float", nullable: false),
                    MaximumNumberOfEquipment = table.Column<double>(type: "float", nullable: false),
                    PurposeOfUse = table.Column<double>(type: "float", nullable: false),
                    SourceUsedIn = table.Column<int>(type: "int", nullable: true),
                    LicenseInventoryLimitsLicenseInventoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmanOrgId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseGenerators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicenseGenerators_LicenseInventoryLimits_LicenseInventoryLimitsLicenseInventoryId",
                        column: x => x.LicenseInventoryLimitsLicenseInventoryId,
                        principalTable: "LicenseInventoryLimits",
                        principalColumn: "LicenseInventoryId");
                    table.ForeignKey(
                        name: "FK_LicenseGenerators_LookupSetTerm_SourceUsedIn",
                        column: x => x.SourceUsedIn,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                });

            migrationBuilder.CreateTable(
                name: "LicenseSealedSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseInventoryId = table.Column<int>(type: "int", nullable: false),
                    NumberOfSources = table.Column<int>(type: "int", nullable: false),
                    MaximumRadioactivity = table.Column<double>(type: "float", nullable: false),
                    PurposeOfUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceUsedIn = table.Column<int>(type: "int", nullable: true),
                    Radionuclide = table.Column<int>(type: "int", nullable: true),
                    MaximumRadioactivityUnit = table.Column<int>(type: "int", nullable: true),
                    LicenseInventoryLimitsLicenseInventoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmanOrgId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseSealedSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicenseSealedSources_LicenseInventoryLimits_LicenseInventoryLimitsLicenseInventoryId",
                        column: x => x.LicenseInventoryLimitsLicenseInventoryId,
                        principalTable: "LicenseInventoryLimits",
                        principalColumn: "LicenseInventoryId");
                    table.ForeignKey(
                        name: "FK_LicenseSealedSources_LookupSetTerm_MaximumRadioactivityUnit",
                        column: x => x.MaximumRadioactivityUnit,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_LicenseSealedSources_LookupSetTerm_Radionuclide",
                        column: x => x.Radionuclide,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_LicenseSealedSources_LookupSetTerm_SourceUsedIn",
                        column: x => x.SourceUsedIn,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                });

            migrationBuilder.CreateTable(
                name: "LicenseUnSealedSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseInventoryId = table.Column<int>(type: "int", nullable: false),
                    NumberOfSources = table.Column<int>(type: "int", nullable: false),
                    MaximumRadioactivity = table.Column<double>(type: "float", nullable: false),
                    PurposeOfUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceUsedIn = table.Column<int>(type: "int", nullable: true),
                    Radionuclide = table.Column<int>(type: "int", nullable: true),
                    PhysicalForm = table.Column<int>(type: "int", nullable: true),
                    LicenseInventoryLimitsLicenseInventoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmanOrgId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseUnSealedSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicenseUnSealedSources_LicenseInventoryLimits_LicenseInventoryLimitsLicenseInventoryId",
                        column: x => x.LicenseInventoryLimitsLicenseInventoryId,
                        principalTable: "LicenseInventoryLimits",
                        principalColumn: "LicenseInventoryId");
                    table.ForeignKey(
                        name: "FK_LicenseUnSealedSources_LookupSetTerm_PhysicalForm",
                        column: x => x.PhysicalForm,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_LicenseUnSealedSources_LookupSetTerm_Radionuclide",
                        column: x => x.Radionuclide,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_LicenseUnSealedSources_LookupSetTerm_SourceUsedIn",
                        column: x => x.SourceUsedIn,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                });

            migrationBuilder.CreateTable(
                name: "LicenseVUnsealedSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseInventoryId = table.Column<int>(type: "int", nullable: false),
                    NumberOfSources = table.Column<int>(type: "int", nullable: false),
                    MaximumRadioactivityInTheWorkPlace = table.Column<double>(type: "float", nullable: false),
                    MaximumRadioactivityPerSource = table.Column<double>(type: "float", nullable: false),
                    PurposeOfUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceUsedIn = table.Column<int>(type: "int", nullable: true),
                    Radionuclide = table.Column<int>(type: "int", nullable: true),
                    PhysicalForm = table.Column<int>(type: "int", nullable: true),
                    UnsealedSourceType = table.Column<int>(type: "int", nullable: true),
                    MaximumRadioactivityPerSourceUnit = table.Column<int>(type: "int", nullable: true),
                    MaximumRadioactivityInTheWorkPlaceUnit = table.Column<int>(type: "int", nullable: true),
                    LicenseInventoryLimitsLicenseInventoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmanOrgId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseVUnsealedSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicenseVUnsealedSources_LicenseInventoryLimits_LicenseInventoryLimitsLicenseInventoryId",
                        column: x => x.LicenseInventoryLimitsLicenseInventoryId,
                        principalTable: "LicenseInventoryLimits",
                        principalColumn: "LicenseInventoryId");
                    table.ForeignKey(
                        name: "FK_LicenseVUnsealedSources_LookupSetTerm_MaximumRadioactivityInTheWorkPlaceUnit",
                        column: x => x.MaximumRadioactivityInTheWorkPlaceUnit,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_LicenseVUnsealedSources_LookupSetTerm_MaximumRadioactivityPerSourceUnit",
                        column: x => x.MaximumRadioactivityPerSourceUnit,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_LicenseVUnsealedSources_LookupSetTerm_PhysicalForm",
                        column: x => x.PhysicalForm,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_LicenseVUnsealedSources_LookupSetTerm_Radionuclide",
                        column: x => x.Radionuclide,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_LicenseVUnsealedSources_LookupSetTerm_SourceUsedIn",
                        column: x => x.SourceUsedIn,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_LicenseVUnsealedSources_LookupSetTerm_UnsealedSourceType",
                        column: x => x.UnsealedSourceType,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                });

            migrationBuilder.CreateTable(
                name: "PermitGenerators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermitInventoryId = table.Column<int>(type: "int", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaximumVoltage = table.Column<double>(type: "float", nullable: false),
                    MaximumTubeCurrent = table.Column<double>(type: "float", nullable: false),
                    MaximumEnergy = table.Column<double>(type: "float", nullable: false),
                    EquipmentType = table.Column<int>(type: "int", nullable: true),
                    ManufactuerName = table.Column<int>(type: "int", nullable: true),
                    PermitInventoryLimitsPermitInventoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmanOrgId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitGenerators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermitGenerators_LookupSetTerm_EquipmentType",
                        column: x => x.EquipmentType,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_PermitGenerators_LookupSetTerm_ManufactuerName",
                        column: x => x.ManufactuerName,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_PermitGenerators_PermitInventoryLimits_PermitInventoryLimitsPermitInventoryId",
                        column: x => x.PermitInventoryLimitsPermitInventoryId,
                        principalTable: "PermitInventoryLimits",
                        principalColumn: "PermitInventoryId");
                });

            migrationBuilder.CreateTable(
                name: "PermitNuclearSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermitInventoryId = table.Column<int>(type: "int", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufactuerName = table.Column<int>(type: "int", nullable: true),
                    EquipmentType = table.Column<int>(type: "int", nullable: true),
                    PermitInventoryLimitsPermitInventoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmanOrgId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitNuclearSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermitNuclearSources_LookupSetTerm_EquipmentType",
                        column: x => x.EquipmentType,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_PermitNuclearSources_LookupSetTerm_ManufactuerName",
                        column: x => x.ManufactuerName,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_PermitNuclearSources_PermitInventoryLimits_PermitInventoryLimitsPermitInventoryId",
                        column: x => x.PermitInventoryLimitsPermitInventoryId,
                        principalTable: "PermitInventoryLimits",
                        principalColumn: "PermitInventoryId");
                });

            migrationBuilder.CreateTable(
                name: "PermitSealedSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermitInventoryId = table.Column<int>(type: "int", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaximumRadioactivity = table.Column<double>(type: "float", nullable: false),
                    ManufactuerName = table.Column<int>(type: "int", nullable: true),
                    Radionuclide = table.Column<int>(type: "int", nullable: true),
                    MaximumRadioactivityUnit = table.Column<int>(type: "int", nullable: true),
                    PermitInventoryLimitsPermitInventoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmanOrgId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitSealedSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermitSealedSources_LookupSetTerm_ManufactuerName",
                        column: x => x.ManufactuerName,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_PermitSealedSources_LookupSetTerm_MaximumRadioactivityUnit",
                        column: x => x.MaximumRadioactivityUnit,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_PermitSealedSources_LookupSetTerm_Radionuclide",
                        column: x => x.Radionuclide,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_PermitSealedSources_PermitInventoryLimits_PermitInventoryLimitsPermitInventoryId",
                        column: x => x.PermitInventoryLimitsPermitInventoryId,
                        principalTable: "PermitInventoryLimits",
                        principalColumn: "PermitInventoryId");
                });

            migrationBuilder.CreateTable(
                name: "PermitUnSealedSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermitInventoryId = table.Column<int>(type: "int", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentActivity = table.Column<double>(type: "float", nullable: false),
                    ManufactuerName = table.Column<int>(type: "int", nullable: true),
                    Radionuclide = table.Column<int>(type: "int", nullable: true),
                    PhysicalForm = table.Column<int>(type: "int", nullable: true),
                    CurrentActivityUnit = table.Column<int>(type: "int", nullable: true),
                    PermitInventoryLimitsPermitInventoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmanOrgId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitUnSealedSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermitUnSealedSources_LookupSetTerm_CurrentActivityUnit",
                        column: x => x.CurrentActivityUnit,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_PermitUnSealedSources_LookupSetTerm_ManufactuerName",
                        column: x => x.ManufactuerName,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_PermitUnSealedSources_LookupSetTerm_PhysicalForm",
                        column: x => x.PhysicalForm,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_PermitUnSealedSources_LookupSetTerm_Radionuclide",
                        column: x => x.Radionuclide,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_PermitUnSealedSources_PermitInventoryLimits_PermitInventoryLimitsPermitInventoryId",
                        column: x => x.PermitInventoryLimitsPermitInventoryId,
                        principalTable: "PermitInventoryLimits",
                        principalColumn: "PermitInventoryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermitInventoryLimits_FacilityId",
                table: "PermitInventoryLimits",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitInventoryLimits_LicenseMasterId",
                table: "PermitInventoryLimits",
                column: "LicenseMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitInventoryLimits_PermitMasterId",
                table: "PermitInventoryLimits",
                column: "PermitMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseProfile_LicensePractices",
                table: "LicenseProfile",
                column: "LicensePractices");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseProfile_PractiseSector",
                table: "LicenseProfile",
                column: "PractiseSector");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseInventoryLimits_FacilityId",
                table: "LicenseInventoryLimits",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseInventoryLimits_LicenseMasterId",
                table: "LicenseInventoryLimits",
                column: "LicenseMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseGenerators_AmanOrgId",
                table: "LicenseGenerators",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseGenerators_LicenseInventoryLimitsLicenseInventoryId",
                table: "LicenseGenerators",
                column: "LicenseInventoryLimitsLicenseInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseGenerators_SourceUsedIn",
                table: "LicenseGenerators",
                column: "SourceUsedIn");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseSealedSources_AmanOrgId",
                table: "LicenseSealedSources",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseSealedSources_LicenseInventoryLimitsLicenseInventoryId",
                table: "LicenseSealedSources",
                column: "LicenseInventoryLimitsLicenseInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseSealedSources_MaximumRadioactivityUnit",
                table: "LicenseSealedSources",
                column: "MaximumRadioactivityUnit");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseSealedSources_Radionuclide",
                table: "LicenseSealedSources",
                column: "Radionuclide");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseSealedSources_SourceUsedIn",
                table: "LicenseSealedSources",
                column: "SourceUsedIn");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseUnSealedSources_AmanOrgId",
                table: "LicenseUnSealedSources",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseUnSealedSources_LicenseInventoryLimitsLicenseInventoryId",
                table: "LicenseUnSealedSources",
                column: "LicenseInventoryLimitsLicenseInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseUnSealedSources_PhysicalForm",
                table: "LicenseUnSealedSources",
                column: "PhysicalForm");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseUnSealedSources_Radionuclide",
                table: "LicenseUnSealedSources",
                column: "Radionuclide");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseUnSealedSources_SourceUsedIn",
                table: "LicenseUnSealedSources",
                column: "SourceUsedIn");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseVUnsealedSources_AmanOrgId",
                table: "LicenseVUnsealedSources",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseVUnsealedSources_LicenseInventoryLimitsLicenseInventoryId",
                table: "LicenseVUnsealedSources",
                column: "LicenseInventoryLimitsLicenseInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseVUnsealedSources_MaximumRadioactivityInTheWorkPlaceUnit",
                table: "LicenseVUnsealedSources",
                column: "MaximumRadioactivityInTheWorkPlaceUnit");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseVUnsealedSources_MaximumRadioactivityPerSourceUnit",
                table: "LicenseVUnsealedSources",
                column: "MaximumRadioactivityPerSourceUnit");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseVUnsealedSources_PhysicalForm",
                table: "LicenseVUnsealedSources",
                column: "PhysicalForm");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseVUnsealedSources_Radionuclide",
                table: "LicenseVUnsealedSources",
                column: "Radionuclide");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseVUnsealedSources_SourceUsedIn",
                table: "LicenseVUnsealedSources",
                column: "SourceUsedIn");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseVUnsealedSources_UnsealedSourceType",
                table: "LicenseVUnsealedSources",
                column: "UnsealedSourceType");

            migrationBuilder.CreateIndex(
                name: "IX_PermitGenerators_AmanOrgId",
                table: "PermitGenerators",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PermitGenerators_EquipmentType",
                table: "PermitGenerators",
                column: "EquipmentType");

            migrationBuilder.CreateIndex(
                name: "IX_PermitGenerators_ManufactuerName",
                table: "PermitGenerators",
                column: "ManufactuerName");

            migrationBuilder.CreateIndex(
                name: "IX_PermitGenerators_PermitInventoryLimitsPermitInventoryId",
                table: "PermitGenerators",
                column: "PermitInventoryLimitsPermitInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitNuclearSources_AmanOrgId",
                table: "PermitNuclearSources",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PermitNuclearSources_EquipmentType",
                table: "PermitNuclearSources",
                column: "EquipmentType");

            migrationBuilder.CreateIndex(
                name: "IX_PermitNuclearSources_ManufactuerName",
                table: "PermitNuclearSources",
                column: "ManufactuerName");

            migrationBuilder.CreateIndex(
                name: "IX_PermitNuclearSources_PermitInventoryLimitsPermitInventoryId",
                table: "PermitNuclearSources",
                column: "PermitInventoryLimitsPermitInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitSealedSources_AmanOrgId",
                table: "PermitSealedSources",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PermitSealedSources_ManufactuerName",
                table: "PermitSealedSources",
                column: "ManufactuerName");

            migrationBuilder.CreateIndex(
                name: "IX_PermitSealedSources_MaximumRadioactivityUnit",
                table: "PermitSealedSources",
                column: "MaximumRadioactivityUnit");

            migrationBuilder.CreateIndex(
                name: "IX_PermitSealedSources_PermitInventoryLimitsPermitInventoryId",
                table: "PermitSealedSources",
                column: "PermitInventoryLimitsPermitInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitSealedSources_Radionuclide",
                table: "PermitSealedSources",
                column: "Radionuclide");

            migrationBuilder.CreateIndex(
                name: "IX_PermitUnSealedSources_AmanOrgId",
                table: "PermitUnSealedSources",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PermitUnSealedSources_CurrentActivityUnit",
                table: "PermitUnSealedSources",
                column: "CurrentActivityUnit");

            migrationBuilder.CreateIndex(
                name: "IX_PermitUnSealedSources_ManufactuerName",
                table: "PermitUnSealedSources",
                column: "ManufactuerName");

            migrationBuilder.CreateIndex(
                name: "IX_PermitUnSealedSources_PermitInventoryLimitsPermitInventoryId",
                table: "PermitUnSealedSources",
                column: "PermitInventoryLimitsPermitInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitUnSealedSources_PhysicalForm",
                table: "PermitUnSealedSources",
                column: "PhysicalForm");

            migrationBuilder.CreateIndex(
                name: "IX_PermitUnSealedSources_Radionuclide",
                table: "PermitUnSealedSources",
                column: "Radionuclide");

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseInventoryLimits_FacilityProfile_FacilityId",
                table: "LicenseInventoryLimits",
                column: "FacilityId",
                principalTable: "FacilityProfile",
                principalColumn: "FacilityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseInventoryLimits_LicenseProfile_LicenseMasterId",
                table: "LicenseInventoryLimits",
                column: "LicenseMasterId",
                principalTable: "LicenseProfile",
                principalColumn: "LicenseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseProfile_LookupSetTerm_LicensePractices",
                table: "LicenseProfile",
                column: "LicensePractices",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseProfile_LookupSetTerm_PractiseSector",
                table: "LicenseProfile",
                column: "PractiseSector",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermitInventoryLimits_FacilityProfile_FacilityId",
                table: "PermitInventoryLimits",
                column: "FacilityId",
                principalTable: "FacilityProfile",
                principalColumn: "FacilityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermitInventoryLimits_LicenseProfile_LicenseMasterId",
                table: "PermitInventoryLimits",
                column: "LicenseMasterId",
                principalTable: "LicenseProfile",
                principalColumn: "LicenseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermitInventoryLimits_PermitDetailsProfile_PermitMasterId",
                table: "PermitInventoryLimits",
                column: "PermitMasterId",
                principalTable: "PermitDetailsProfile",
                principalColumn: "PermitDetailsId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LicenseInventoryLimits_FacilityProfile_FacilityId",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropForeignKey(
                name: "FK_LicenseInventoryLimits_LicenseProfile_LicenseMasterId",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropForeignKey(
                name: "FK_LicenseProfile_LookupSetTerm_LicensePractices",
                table: "LicenseProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_LicenseProfile_LookupSetTerm_PractiseSector",
                table: "LicenseProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_PermitInventoryLimits_FacilityProfile_FacilityId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropForeignKey(
                name: "FK_PermitInventoryLimits_LicenseProfile_LicenseMasterId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropForeignKey(
                name: "FK_PermitInventoryLimits_PermitDetailsProfile_PermitMasterId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropTable(
                name: "LicenseGenerators");

            migrationBuilder.DropTable(
                name: "LicenseSealedSources");

            migrationBuilder.DropTable(
                name: "LicenseUnSealedSources");

            migrationBuilder.DropTable(
                name: "LicenseVUnsealedSources");

            migrationBuilder.DropTable(
                name: "PermitGenerators");

            migrationBuilder.DropTable(
                name: "PermitNuclearSources");

            migrationBuilder.DropTable(
                name: "PermitSealedSources");

            migrationBuilder.DropTable(
                name: "PermitUnSealedSources");

            migrationBuilder.DropIndex(
                name: "IX_PermitInventoryLimits_FacilityId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropIndex(
                name: "IX_PermitInventoryLimits_LicenseMasterId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropIndex(
                name: "IX_PermitInventoryLimits_PermitMasterId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropIndex(
                name: "IX_LicenseProfile_LicensePractices",
                table: "LicenseProfile");

            migrationBuilder.DropIndex(
                name: "IX_LicenseProfile_PractiseSector",
                table: "LicenseProfile");

            migrationBuilder.DropIndex(
                name: "IX_LicenseInventoryLimits_FacilityId",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropIndex(
                name: "IX_LicenseInventoryLimits_LicenseMasterId",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "LicenseMasterId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "PermitMasterId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "LicensePractices",
                table: "LicenseProfile");

            migrationBuilder.RenameColumn(
                name: "LicenseMasterId",
                table: "LicenseInventoryLimits",
                newName: "NumberofSources");

            migrationBuilder.RenameColumn(
                name: "FacilityId",
                table: "LicenseInventoryLimits",
                newName: "MaximumRadioactivity");

            migrationBuilder.AddColumn<DateTime>(
                name: "AmanInsertedOn",
                table: "PermitInventoryLimits",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManufactureName",
                table: "PermitInventoryLimits",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelMaximumRadioactivity",
                table: "PermitInventoryLimits",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PermitDetailsId",
                table: "PermitInventoryLimits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Radionuclide",
                table: "PermitInventoryLimits",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceSerialNo",
                table: "PermitInventoryLimits",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "PermitInventoryLimits",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PractiseSector",
                table: "LicenseProfile",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AmanInsertedOn",
                table: "LicenseInventoryLimits",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LicenseId",
                table: "LicenseInventoryLimits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Radionuclide",
                table: "LicenseInventoryLimits",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceType",
                table: "LicenseInventoryLimits",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Province",
                table: "FacilityProfile",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "FacilityProfile",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermitInventoryLimits_PermitDetailsId",
                table: "PermitInventoryLimits",
                column: "PermitDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseInventoryLimits_LicenseId",
                table: "LicenseInventoryLimits",
                column: "LicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseInventoryLimits_LicenseProfile_LicenseId",
                table: "LicenseInventoryLimits",
                column: "LicenseId",
                principalTable: "LicenseProfile",
                principalColumn: "LicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermitInventoryLimits_PermitDetailsProfile_PermitDetailsId",
                table: "PermitInventoryLimits",
                column: "PermitDetailsId",
                principalTable: "PermitDetailsProfile",
                principalColumn: "PermitDetailsId");
        }
    }
}
