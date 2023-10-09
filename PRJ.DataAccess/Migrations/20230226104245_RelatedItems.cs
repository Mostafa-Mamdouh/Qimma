using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class RelatedItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedItemFiles_RelatedItems_Attachments",
                table: "RelatedItemFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedItemFiles_RelatedItems_RealtedItemCode",
                table: "RelatedItemFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedItems_LookupSetTerm_ItemCategory",
                table: "RelatedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedItems_ManufacturerMaster_Manufacturer",
                table: "RelatedItems");

            migrationBuilder.DropIndex(
                name: "IX_RelatedItemFiles_Attachments",
                table: "RelatedItemFiles");

            migrationBuilder.DropIndex(
                name: "IX_RelatedItemFiles_RealtedItemCode",
                table: "RelatedItemFiles");

            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "DateOfManufacturing",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "EndUserCertificate",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "GeneralDetailFlag",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "GovernmentCommitments",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "HSCode",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "ItemStructureCode",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "ItemTypeName",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "ItemTypeNumber",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "PermitinitialQty",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "StructureLevelNum",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "RelatedItemFiles");

            migrationBuilder.DropColumn(
                name: "RealtedItemCode",
                table: "RelatedItemFiles");

            migrationBuilder.DropColumn(
                name: "RelatedItemFileId",
                table: "RelatedItemFiles");

            migrationBuilder.RenameColumn(
                name: "NuclearEntityId",
                table: "RelatedItems",
                newName: "PermitdetailsId");

            migrationBuilder.RenameColumn(
                name: "Manufacturer",
                table: "RelatedItems",
                newName: "ManufacturerId");

            migrationBuilder.RenameColumn(
                name: "ItemCategory",
                table: "RelatedItems",
                newName: "ManufacturerCountryId");

            migrationBuilder.RenameIndex(
                name: "IX_RelatedItems_Manufacturer",
                table: "RelatedItems",
                newName: "IX_RelatedItems_ManufacturerId");

            migrationBuilder.RenameIndex(
                name: "IX_RelatedItems_ItemCategory",
                table: "RelatedItems",
                newName: "IX_RelatedItems_ManufacturerCountryId");

            migrationBuilder.AddColumn<int>(
                name: "EntityId",
                table: "RelatedItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacilityId",
                table: "RelatedItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HierarchyStructureCode",
                table: "RelatedItems",
                type: "nvarchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTechnologyAndSoftware",
                table: "RelatedItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LicenseId",
                table: "RelatedItems",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ParentStructure",
                table: "RelatedItemHierarchyStructure",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemStructureLongDes",
                table: "RelatedItemHierarchyStructure",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemStructureCode",
                table: "RelatedItemHierarchyStructure",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "HSCode",
                table: "RelatedItemHierarchyStructure",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemNo",
                table: "RelatedItemHierarchyStructure",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedItemCode",
                table: "RelatedItemFiles",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

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
                name: "IX_RelatedItems_PermitdetailsId",
                table: "RelatedItems",
                column: "PermitdetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItemHierarchyStructure_ItemNo",
                table: "RelatedItemHierarchyStructure",
                column: "ItemNo",
                unique: true,
                filter: "[ItemNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItemFiles_RelatedItemCode",
                table: "RelatedItemFiles",
                column: "RelatedItemCode");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedItemFiles_RelatedItems_RelatedItemCode",
                table: "RelatedItemFiles",
                column: "RelatedItemCode",
                principalTable: "RelatedItems",
                principalColumn: "RelatedItemCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedItems_EntityProfile_EntityId",
                table: "RelatedItems",
                column: "EntityId",
                principalTable: "EntityProfile",
                principalColumn: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedItems_FacilityProfile_FacilityId",
                table: "RelatedItems",
                column: "FacilityId",
                principalTable: "FacilityProfile",
                principalColumn: "FacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedItems_LicenseProfile_LicenseId",
                table: "RelatedItems",
                column: "LicenseId",
                principalTable: "LicenseProfile",
                principalColumn: "LicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedItems_LookupSetTerm_ManufacturerCountryId",
                table: "RelatedItems",
                column: "ManufacturerCountryId",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedItems_LookupSetTerm_ManufacturerId",
                table: "RelatedItems",
                column: "ManufacturerId",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedItems_PermitDetailsProfile_PermitdetailsId",
                table: "RelatedItems",
                column: "PermitdetailsId",
                principalTable: "PermitDetailsProfile",
                principalColumn: "PermitDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedItems_RelatedItemHierarchyStructure_HierarchyStructureCode",
                table: "RelatedItems",
                column: "HierarchyStructureCode",
                principalTable: "RelatedItemHierarchyStructure",
                principalColumn: "ItemStructureCode",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedItemFiles_RelatedItems_RelatedItemCode",
                table: "RelatedItemFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedItems_EntityProfile_EntityId",
                table: "RelatedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedItems_FacilityProfile_FacilityId",
                table: "RelatedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedItems_LicenseProfile_LicenseId",
                table: "RelatedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedItems_LookupSetTerm_ManufacturerCountryId",
                table: "RelatedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedItems_LookupSetTerm_ManufacturerId",
                table: "RelatedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedItems_PermitDetailsProfile_PermitdetailsId",
                table: "RelatedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedItems_RelatedItemHierarchyStructure_HierarchyStructureCode",
                table: "RelatedItems");

            migrationBuilder.DropIndex(
                name: "IX_RelatedItems_EntityId",
                table: "RelatedItems");

            migrationBuilder.DropIndex(
                name: "IX_RelatedItems_FacilityId",
                table: "RelatedItems");

            migrationBuilder.DropIndex(
                name: "IX_RelatedItems_HierarchyStructureCode",
                table: "RelatedItems");

            migrationBuilder.DropIndex(
                name: "IX_RelatedItems_LicenseId",
                table: "RelatedItems");

            migrationBuilder.DropIndex(
                name: "IX_RelatedItems_PermitdetailsId",
                table: "RelatedItems");

            migrationBuilder.DropIndex(
                name: "IX_RelatedItemHierarchyStructure_ItemNo",
                table: "RelatedItemHierarchyStructure");

            migrationBuilder.DropIndex(
                name: "IX_RelatedItemFiles_RelatedItemCode",
                table: "RelatedItemFiles");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "HierarchyStructureCode",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "IsTechnologyAndSoftware",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "HSCode",
                table: "RelatedItemHierarchyStructure");

            migrationBuilder.DropColumn(
                name: "ItemNo",
                table: "RelatedItemHierarchyStructure");

            migrationBuilder.DropColumn(
                name: "RelatedItemCode",
                table: "RelatedItemFiles");

            migrationBuilder.RenameColumn(
                name: "PermitdetailsId",
                table: "RelatedItems",
                newName: "NuclearEntityId");

            migrationBuilder.RenameColumn(
                name: "ManufacturerId",
                table: "RelatedItems",
                newName: "Manufacturer");

            migrationBuilder.RenameColumn(
                name: "ManufacturerCountryId",
                table: "RelatedItems",
                newName: "ItemCategory");

            migrationBuilder.RenameIndex(
                name: "IX_RelatedItems_ManufacturerId",
                table: "RelatedItems",
                newName: "IX_RelatedItems_Manufacturer");

            migrationBuilder.RenameIndex(
                name: "IX_RelatedItems_ManufacturerCountryId",
                table: "RelatedItems",
                newName: "IX_RelatedItems_ItemCategory");

            migrationBuilder.AddColumn<string>(
                name: "Attachments",
                table: "RelatedItems",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfManufacturing",
                table: "RelatedItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EndUserCertificate",
                table: "RelatedItems",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GeneralDetailFlag",
                table: "RelatedItems",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GovernmentCommitments",
                table: "RelatedItems",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HSCode",
                table: "RelatedItems",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemStructureCode",
                table: "RelatedItems",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemTypeName",
                table: "RelatedItems",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemTypeNumber",
                table: "RelatedItems",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "RelatedItems",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PermitinitialQty",
                table: "RelatedItems",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "RelatedItems",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StructureLevelNum",
                table: "RelatedItems",
                type: "int",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ParentStructure",
                table: "RelatedItemHierarchyStructure",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemStructureLongDes",
                table: "RelatedItemHierarchyStructure",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemStructureCode",
                table: "RelatedItemHierarchyStructure",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "Attachments",
                table: "RelatedItemFiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RealtedItemCode",
                table: "RelatedItemFiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedItemFileId",
                table: "RelatedItemFiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItemFiles_Attachments",
                table: "RelatedItemFiles",
                column: "Attachments");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItemFiles_RealtedItemCode",
                table: "RelatedItemFiles",
                column: "RealtedItemCode");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedItemFiles_RelatedItems_Attachments",
                table: "RelatedItemFiles",
                column: "Attachments",
                principalTable: "RelatedItems",
                principalColumn: "RelatedItemCode");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedItemFiles_RelatedItems_RealtedItemCode",
                table: "RelatedItemFiles",
                column: "RealtedItemCode",
                principalTable: "RelatedItems",
                principalColumn: "RelatedItemCode");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedItems_LookupSetTerm_ItemCategory",
                table: "RelatedItems",
                column: "ItemCategory",
                principalTable: "LookupSetTerm",
                principalColumn: "LookupSetTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedItems_ManufacturerMaster_Manufacturer",
                table: "RelatedItems",
                column: "Manufacturer",
                principalTable: "ManufacturerMaster",
                principalColumn: "ManufacturerId");
        }
    }
}
