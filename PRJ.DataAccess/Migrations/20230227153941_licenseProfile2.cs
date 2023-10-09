using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class licenseProfile2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacilityProfile_LookupSetTerm_CityId",
                table: "FacilityProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_LicenseProfile_LookupSetTerm_LicensePractices",
                table: "LicenseProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedItemFiles_RelatedItems_RelatedItemCode",
                table: "RelatedItemFiles");

            migrationBuilder.DropTable(
                name: "RelatedItems");

            migrationBuilder.DropTable(
                name: "RelatedItemHierarchyStructure");

            migrationBuilder.DropIndex(
                name: "IX_LicenseProfile_LicensePractices",
                table: "LicenseProfile");

            migrationBuilder.DropIndex(
                name: "IX_FacilityProfile_CityId",
                table: "FacilityProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RelatedItemFiles",
                table: "RelatedItemFiles");

            migrationBuilder.DropColumn(
                name: "LicensePractices",
                table: "LicenseProfile");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "FacilityProfile");

            migrationBuilder.RenameTable(
                name: "RelatedItemFiles",
                newName: "RelatedItemsFiles");

            migrationBuilder.RenameIndex(
                name: "IX_RelatedItemFiles_RelatedItemCode",
                table: "RelatedItemsFiles",
                newName: "IX_RelatedItemsFiles_RelatedItemCode");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "FacilityProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelatedItemsFiles",
                table: "RelatedItemsFiles",
                column: "FileId");

            migrationBuilder.CreateTable(
                name: "LicenseProfilePractice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracticeLookup = table.Column<int>(type: "int", nullable: false),
                    LicenseProfileId = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseProfilePractice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicenseProfilePractice_LicenseProfile_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "LicenseProfile",
                        principalColumn: "LicenseId");
                    table.ForeignKey(
                        name: "FK_LicenseProfilePractice_LicenseProfile_LicenseProfileId",
                        column: x => x.LicenseProfileId,
                        principalTable: "LicenseProfile",
                        principalColumn: "LicenseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicenseProfilePractice_LookupSetTerm_PracticeLookup",
                        column: x => x.PracticeLookup,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelatedItemsHierarchyStructure",
                columns: table => new
                {
                    ItemStructureCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ItemStructureDesFrn = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ItemStructureDesNtv = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StructureLevelNum = table.Column<int>(type: "int", nullable: false),
                    StructureGeneralDetailFlag = table.Column<int>(type: "int", nullable: false),
                    ParentStructure = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ItemStructureLongDes = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ItemNo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    HSCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedItemsHierarchyStructure", x => x.ItemStructureCode);
                });

            migrationBuilder.CreateTable(
                name: "RelatedItem",
                columns: table => new
                {
                    RelatedItemCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ManufacturerSerialNom = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    FacilityRelatedItemID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    IsTechnologyAndSoftware = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    FacilityId = table.Column<int>(type: "int", nullable: true),
                    LicenseId = table.Column<int>(type: "int", nullable: true),
                    PermitdetailsId = table.Column<int>(type: "int", nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: true),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    ManufacturerCountryId = table.Column<int>(type: "int", nullable: true),
                    HierarchyStructureCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedItem", x => x.RelatedItemCode);
                    table.ForeignKey(
                        name: "FK_RelatedItem_EntityProfile_EntityId",
                        column: x => x.EntityId,
                        principalTable: "EntityProfile",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_RelatedItem_FacilityProfile_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "FacilityProfile",
                        principalColumn: "FacilityId");
                    table.ForeignKey(
                        name: "FK_RelatedItem_LicenseProfile_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "LicenseProfile",
                        principalColumn: "LicenseId");
                    table.ForeignKey(
                        name: "FK_RelatedItem_LookupSetTerm_ManufacturerCountryId",
                        column: x => x.ManufacturerCountryId,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_RelatedItem_LookupSetTerm_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_RelatedItem_LookupSetTerm_StatusID",
                        column: x => x.StatusID,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_RelatedItem_PermitDetailsProfile_PermitdetailsId",
                        column: x => x.PermitdetailsId,
                        principalTable: "PermitDetailsProfile",
                        principalColumn: "PermitDetailsId");
                    table.ForeignKey(
                        name: "FK_RelatedItem_RelatedItemsHierarchyStructure_HierarchyStructureCode",
                        column: x => x.HierarchyStructureCode,
                        principalTable: "RelatedItemsHierarchyStructure",
                        principalColumn: "ItemStructureCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LicenseProfilePractice_LicenseId",
                table: "LicenseProfilePractice",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseProfilePractice_LicenseProfileId",
                table: "LicenseProfilePractice",
                column: "LicenseProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseProfilePractice_PracticeLookup",
                table: "LicenseProfilePractice",
                column: "PracticeLookup");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItem_EntityId",
                table: "RelatedItem",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItem_FacilityId",
                table: "RelatedItem",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItem_HierarchyStructureCode",
                table: "RelatedItem",
                column: "HierarchyStructureCode");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItem_LicenseId",
                table: "RelatedItem",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItem_ManufacturerCountryId",
                table: "RelatedItem",
                column: "ManufacturerCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItem_ManufacturerId",
                table: "RelatedItem",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItem_PermitdetailsId",
                table: "RelatedItem",
                column: "PermitdetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItem_StatusID",
                table: "RelatedItem",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItemsHierarchyStructure_ItemNo",
                table: "RelatedItemsHierarchyStructure",
                column: "ItemNo",
                unique: true,
                filter: "[ItemNo] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedItemsFiles_RelatedItem_RelatedItemCode",
                table: "RelatedItemsFiles",
                column: "RelatedItemCode",
                principalTable: "RelatedItem",
                principalColumn: "RelatedItemCode",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedItemsFiles_RelatedItem_RelatedItemCode",
                table: "RelatedItemsFiles");

            migrationBuilder.DropTable(
                name: "LicenseProfilePractice");

            migrationBuilder.DropTable(
                name: "RelatedItem");

            migrationBuilder.DropTable(
                name: "RelatedItemsHierarchyStructure");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RelatedItemsFiles",
                table: "RelatedItemsFiles");

            migrationBuilder.DropColumn(
                name: "City",
                table: "FacilityProfile");

            migrationBuilder.RenameTable(
                name: "RelatedItemsFiles",
                newName: "RelatedItemFiles");

            migrationBuilder.RenameIndex(
                name: "IX_RelatedItemsFiles_RelatedItemCode",
                table: "RelatedItemFiles",
                newName: "IX_RelatedItemFiles_RelatedItemCode");

            migrationBuilder.AddColumn<int>(
                name: "LicensePractices",
                table: "LicenseProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "FacilityProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelatedItemFiles",
                table: "RelatedItemFiles",
                column: "FileId");

            migrationBuilder.CreateTable(
                name: "RelatedItemHierarchyStructure",
                columns: table => new
                {
                    ItemStructureCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HSCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ItemNo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ItemStructureDesFrn = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ItemStructureDesNtv = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ItemStructureLongDes = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ParentStructure = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    StructureGeneralDetailFlag = table.Column<int>(type: "int", nullable: false),
                    StructureLevelNum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedItemHierarchyStructure", x => x.ItemStructureCode);
                });

            migrationBuilder.CreateTable(
                name: "RelatedItems",
                columns: table => new
                {
                    RelatedItemCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    FacilityId = table.Column<int>(type: "int", nullable: true),
                    HierarchyStructureCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    LicenseId = table.Column<int>(type: "int", nullable: true),
                    ManufacturerCountryId = table.Column<int>(type: "int", nullable: true),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    PermitdetailsId = table.Column<int>(type: "int", nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    FacilityRelatedItemID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    IsTechnologyAndSoftware = table.Column<bool>(type: "bit", nullable: false),
                    ManufacturerSerialNom = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedItems", x => x.RelatedItemCode);
                    table.ForeignKey(
                        name: "FK_RelatedItems_EntityProfile_EntityId",
                        column: x => x.EntityId,
                        principalTable: "EntityProfile",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_RelatedItems_FacilityProfile_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "FacilityProfile",
                        principalColumn: "FacilityId");
                    table.ForeignKey(
                        name: "FK_RelatedItems_LicenseProfile_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "LicenseProfile",
                        principalColumn: "LicenseId");
                    table.ForeignKey(
                        name: "FK_RelatedItems_LookupSetTerm_ManufacturerCountryId",
                        column: x => x.ManufacturerCountryId,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_RelatedItems_LookupSetTerm_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_RelatedItems_LookupSetTerm_StatusID",
                        column: x => x.StatusID,
                        principalTable: "LookupSetTerm",
                        principalColumn: "LookupSetTermId");
                    table.ForeignKey(
                        name: "FK_RelatedItems_PermitDetailsProfile_PermitdetailsId",
                        column: x => x.PermitdetailsId,
                        principalTable: "PermitDetailsProfile",
                        principalColumn: "PermitDetailsId");
                    table.ForeignKey(
                        name: "FK_RelatedItems_RelatedItemHierarchyStructure_HierarchyStructureCode",
                        column: x => x.HierarchyStructureCode,
                        principalTable: "RelatedItemHierarchyStructure",
                        principalColumn: "ItemStructureCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LicenseProfile_LicensePractices",
                table: "LicenseProfile",
                column: "LicensePractices");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityProfile_CityId",
                table: "FacilityProfile",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItemHierarchyStructure_ItemNo",
                table: "RelatedItemHierarchyStructure",
                column: "ItemNo",
                unique: true,
                filter: "[ItemNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItems_EntityId",
                table: "RelatedItems",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItems_FacilityId",
                table: "RelatedItems",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItems_HierarchyStructureCode",
                table: "RelatedItems",
                column: "HierarchyStructureCode");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItems_LicenseId",
                table: "RelatedItems",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItems_ManufacturerCountryId",
                table: "RelatedItems",
                column: "ManufacturerCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItems_ManufacturerId",
                table: "RelatedItems",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItems_PermitdetailsId",
                table: "RelatedItems",
                column: "PermitdetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItems_StatusID",
                table: "RelatedItems",
                column: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_FacilityProfile_LookupSetTerm_CityId",
                table: "FacilityProfile",
                column: "CityId",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseProfile_LookupSetTerm_LicensePractices",
                table: "LicenseProfile",
                column: "LicensePractices",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedItemFiles_RelatedItems_RelatedItemCode",
                table: "RelatedItemFiles",
                column: "RelatedItemCode",
                principalTable: "RelatedItems",
                principalColumn: "RelatedItemCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
