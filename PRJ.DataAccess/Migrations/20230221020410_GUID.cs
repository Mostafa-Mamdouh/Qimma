using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class GUID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LicenseInventoryLimits_FacilityProfile_FacilityId",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropForeignKey(
                name: "FK_LicenseSealedSources_LicenseInventoryLimits_LicenseInventoryLimitsLicenseInventoryId",
                table: "LicenseSealedSources");

            migrationBuilder.DropForeignKey(
                name: "FK_PermitInventoryLimits_FacilityProfile_FacilityId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropIndex(
                name: "IX_WorkersExposuresDoses_AmanOrgId",
                table: "WorkersExposuresDoses");

            migrationBuilder.DropIndex(
                name: "IX_WorkersDosimeters_AmanOrgId",
                table: "WorkersDosimeters");

            migrationBuilder.DropIndex(
                name: "IX_Workers_AmanOrgId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_TrnItemSourceRadionuclides_AmanOrgId",
                table: "TrnItemSourceRadionuclides");

            migrationBuilder.DropIndex(
                name: "IX_TrnItemSourceHistory_AmanOrgId",
                table: "TrnItemSourceHistory");

            migrationBuilder.DropIndex(
                name: "IX_TrnItemSource_AmanOrgId",
                table: "TrnItemSource");

            migrationBuilder.DropIndex(
                name: "IX_TrnItemFiles_AmanOrgId",
                table: "TrnItemFiles");

            migrationBuilder.DropIndex(
                name: "IX_TransactionTypeMaster_AmanOrgId",
                table: "TransactionTypeMaster");

            migrationBuilder.DropIndex(
                name: "IX_TransactionHeader_AmanOrgId",
                table: "TransactionHeader");

            migrationBuilder.DropIndex(
                name: "IX_ServiceItemProfile_AmanOrgId",
                table: "ServiceItemProfile");

            migrationBuilder.DropIndex(
                name: "IX_ServiceItemPrice_AmanOrgId",
                table: "ServiceItemPrice");

            migrationBuilder.DropIndex(
                name: "IX_SafetyResponsibleOfficersProfile_AmanOrgId",
                table: "SafetyResponsibleOfficersProfile");

            migrationBuilder.DropIndex(
                name: "IX_RelatedItems_AmanOrgId",
                table: "RelatedItems");

            migrationBuilder.DropIndex(
                name: "IX_RelatedItemHierarchyStructure_AmanOrgId",
                table: "RelatedItemHierarchyStructure");

            migrationBuilder.DropIndex(
                name: "IX_RelatedItemFiles_AmanOrgId",
                table: "RelatedItemFiles");

            migrationBuilder.DropIndex(
                name: "IX_Radionuclides_AmanOrgId",
                table: "Radionuclides");

            migrationBuilder.DropIndex(
                name: "IX_RadiationGeneratorsProfile_AmanOrgId",
                table: "RadiationGeneratorsProfile");

            migrationBuilder.DropIndex(
                name: "IX_PractiseProfile_AmanOrgId",
                table: "PractiseProfile");

            migrationBuilder.DropIndex(
                name: "IX_PermitUnSealedSources_AmanOrgId",
                table: "PermitUnSealedSources");

            migrationBuilder.DropIndex(
                name: "IX_PermitSealedSources_AmanOrgId",
                table: "PermitSealedSources");

            migrationBuilder.DropIndex(
                name: "IX_PermitNuclearSources_AmanOrgId",
                table: "PermitNuclearSources");

            migrationBuilder.DropIndex(
                name: "IX_PermitInventoryLimits_AmanOrgId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropIndex(
                name: "IX_PermitInventoryLimits_FacilityId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropIndex(
                name: "IX_PermitGenerators_AmanOrgId",
                table: "PermitGenerators");

            migrationBuilder.DropIndex(
                name: "IX_PermitDetailsProfile_AmanOrgId",
                table: "PermitDetailsProfile");

            migrationBuilder.DropIndex(
                name: "IX_NuclearRelatedItemsProfile_AmanOrgId",
                table: "NuclearRelatedItemsProfile");

            migrationBuilder.DropIndex(
                name: "IX_NuclearMaterials_AmanOrgId",
                table: "NuclearMaterials");

            migrationBuilder.DropIndex(
                name: "IX_NuclearMaterialRadionulcides_AmanOrgId",
                table: "NuclearMaterialRadionulcides");

            migrationBuilder.DropIndex(
                name: "IX_NuclearMaterialFiles_AmanOrgId",
                table: "NuclearMaterialFiles");

            migrationBuilder.DropIndex(
                name: "IX_ManufacturerMaster_AmanOrgId",
                table: "ManufacturerMaster");

     
            migrationBuilder.DropIndex(
                name: "IX_LookupSetTerm_LookupSetId_AmanOrgId",
                table: "LookupSetTerm");

            migrationBuilder.DropIndex(
                name: "IX_LookupSet_AmanOrgId",
                table: "LookupSet");

            migrationBuilder.DropIndex(
                name: "IX_ListOfValues_AmanOrgId",
                table: "ListOfValues");

            migrationBuilder.DropIndex(
                name: "IX_LicenseVUnsealedSources_AmanOrgId",
                table: "LicenseVUnsealedSources");

            migrationBuilder.DropIndex(
                name: "IX_LicenseUnSealedSources_AmanOrgId",
                table: "LicenseUnSealedSources");

            migrationBuilder.DropIndex(
                name: "IX_LicenseSealedSources_AmanOrgId",
                table: "LicenseSealedSources");

            migrationBuilder.DropIndex(
                name: "IX_LicenseSealedSources_LicenseInventoryLimitsLicenseInventoryId",
                table: "LicenseSealedSources");

            migrationBuilder.DropIndex(
                name: "IX_LicenseProfile_AmanOrgId",
                table: "LicenseProfile");

            migrationBuilder.DropIndex(
                name: "IX_LicenseInventoryLimits_AmanOrgId",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropIndex(
                name: "IX_LicenseInventoryLimits_FacilityId",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropIndex(
                name: "IX_LicenseGenerators_AmanOrgId",
                table: "LicenseGenerators");

            migrationBuilder.DropIndex(
                name: "IX_LegalRepresentativesProfile_AmanOrgId",
                table: "LegalRepresentativesProfile");

            migrationBuilder.DropIndex(
                name: "IX_ItemSourceStatusHistory_AmanOrgId",
                table: "ItemSourceStatusHistory");

            migrationBuilder.DropIndex(
                name: "IX_ItemSourceStatus_AmanOrgId",
                table: "ItemSourceStatus");

            migrationBuilder.DropIndex(
                name: "IX_ItemSourceRadionulcides_AmanOrgId",
                table: "ItemSourceRadionulcides");

            migrationBuilder.DropIndex(
                name: "IX_ItemSourceProfile_AmanOrgId",
                table: "ItemSourceProfile");

            migrationBuilder.DropIndex(
                name: "IX_ItemSourceMsgHistory_AmanOrgId",
                table: "ItemSourceMsgHistory");

            migrationBuilder.DropIndex(
                name: "IX_ItemSourceFiles_AmanOrgId",
                table: "ItemSourceFiles");

            migrationBuilder.DropIndex(
                name: "IX_ItemHierarchyStructure_AmanOrgId",
                table: "ItemHierarchyStructure");

            migrationBuilder.DropIndex(
                name: "IX_InternalScreens_AmanOrgId",
                table: "InternalScreens");

            migrationBuilder.DropIndex(
                name: "IX_InternalScreenRoles_AmanOrgId",
                table: "InternalScreenRoles");

            migrationBuilder.DropIndex(
                name: "IX_InternalScreenFields_AmanOrgId",
                table: "InternalScreenFields");

            migrationBuilder.DropIndex(
                name: "IX_InternalRoles_AmanOrgId",
                table: "InternalRoles");

            migrationBuilder.DropIndex(
                name: "IX_InternalFieldPermissions_AmanOrgId",
                table: "InternalFieldPermissions");

            migrationBuilder.DropIndex(
                name: "IX_FacilityProfile_AmanOrgId",
                table: "FacilityProfile");

            migrationBuilder.DropIndex(
                name: "IX_EntityProfile_AmanOrgId",
                table: "EntityProfile");

            migrationBuilder.DropIndex(
                name: "IX_CustomerProfile_AmanOrgId",
                table: "CustomerProfile");

            migrationBuilder.DropIndex(
                name: "IX_BillingServiceTrnHeader_AmanOrgId",
                table: "BillingServiceTrnHeader");

            migrationBuilder.DropIndex(
                name: "IX_BasCountries_AmanOrgId",
                table: "BasCountries");

            migrationBuilder.DropIndex(
                name: "IX_BasCities_AmanOrgId",
                table: "BasCities");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "WorkersExposuresDoses");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "WorkersDosimeters");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "TrnItemSourceRadionuclides");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "TrnItemSourceHistory");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "TrnItemFiles");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "TransactionTypeMaster");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "TransactionHeader");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "ServiceItemProfile");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "ServiceItemPrice");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "SafetyResponsibleOfficersProfile");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "RelatedItemHierarchyStructure");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "RelatedItemFiles");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "Radionuclides");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "RadiationGeneratorsProfile");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "PractiseProfile");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "PermitUnSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "PermitSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "PermitNuclearSources");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "PermitGenerators");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "PermitDetailsProfile");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "NuclearRelatedItemsProfile");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "NuclearMaterials");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "NuclearMaterialRadionulcides");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "NuclearMaterialFiles");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "ManufacturerMaster");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "LookupSet");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "ListOfValues");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "LicenseVUnsealedSources");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "LicenseUnSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "LicenseSealedSources");

            migrationBuilder.DropColumn(
                name: "LicenseInventoryLimitsLicenseInventoryId",
                table: "LicenseSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "LicenseProfile");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "LicenseGenerators");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "LegalRepresentativesProfile");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "ItemSourceStatusHistory");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "ItemSourceStatus");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "ItemSourceRadionulcides");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "ItemSourceProfile");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "ItemSourceMsgHistory");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "ItemSourceFiles");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "ItemHierarchyStructure");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "InternalScreens");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "InternalScreenRoles");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "InternalScreenFields");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "InternalRoles");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "InternalFieldPermissions");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "FacilityProfile");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "EntityProfile");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "CustomerProfile");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "BillingServiceTrnHeader");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "BasCountries");

            migrationBuilder.DropColumn(
                name: "AmanOrgId",
                table: "BasCities");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "LookupSetTerm",
                newName: "AmanId");

            migrationBuilder.AddColumn<string>(
                name: "AmanId",
                table: "SafetyResponsibleOfficersProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanId",
                table: "PermitUnSealedSources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AmanInsertedOn",
                table: "PermitUnSealedSources",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanPermitInventoryId",
                table: "PermitUnSealedSources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanId",
                table: "PermitSealedSources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AmanInsertedOn",
                table: "PermitSealedSources",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanPermitInventoryId",
                table: "PermitSealedSources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanId",
                table: "PermitNuclearSources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AmanInsertedOn",
                table: "PermitNuclearSources",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanPermitInventoryId",
                table: "PermitNuclearSources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanId",
                table: "PermitInventoryLimits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AmanInsertedOn",
                table: "PermitInventoryLimits",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanLicenseMasterId",
                table: "PermitInventoryLimits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanPermitMasterId",
                table: "PermitInventoryLimits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanId",
                table: "PermitGenerators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AmanInsertedOn",
                table: "PermitGenerators",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanPermitInventoryId",
                table: "PermitGenerators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanFacilityId",
                table: "PermitDetailsProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanId",
                table: "PermitDetailsProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanLicenseId",
                table: "PermitDetailsProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanId",
                table: "LicenseVUnsealedSources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AmanInsertedOn",
                table: "LicenseVUnsealedSources",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanLicenseInventoryId",
                table: "LicenseVUnsealedSources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanId",
                table: "LicenseUnSealedSources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AmanInsertedOn",
                table: "LicenseUnSealedSources",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanLicenseInventoryId",
                table: "LicenseUnSealedSources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanId",
                table: "LicenseSealedSources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AmanInsertedOn",
                table: "LicenseSealedSources",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanLicenseInventoryId",
                table: "LicenseSealedSources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanEntityId",
                table: "LicenseProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanFacilityId",
                table: "LicenseProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanId",
                table: "LicenseProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanId",
                table: "LicenseInventoryLimits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AmanInsertedOn",
                table: "LicenseInventoryLimits",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanLicenseMasterId",
                table: "LicenseInventoryLimits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PurposeOfUse",
                table: "LicenseGenerators",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "AmanId",
                table: "LicenseGenerators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AmanInsertedOn",
                table: "LicenseGenerators",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanLicenseInventoryId",
                table: "LicenseGenerators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanId",
                table: "LegalRepresentativesProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AmanFacilityId",
                table: "FacilityProfile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AmanId",
                table: "FacilityProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmanId",
                table: "EntityProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LookupSetTerm_LookupSetId_AmanId",
                table: "LookupSetTerm",
                columns: new[] { "LookupSetId", "AmanId" },
                unique: true,
                filter: "[AmanId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseSealedSources_LicenseInventoryId",
                table: "LicenseSealedSources",
                column: "LicenseInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseSealedSources_LicenseInventoryLimits_LicenseInventoryId",
                table: "LicenseSealedSources",
                column: "LicenseInventoryId",
                principalTable: "LicenseInventoryLimits",
                principalColumn: "LicenseInventoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LicenseSealedSources_LicenseInventoryLimits_LicenseInventoryId",
                table: "LicenseSealedSources");

            migrationBuilder.DropIndex(
                name: "IX_LookupSetTerm_LookupSetId_AmanId",
                table: "LookupSetTerm");

            migrationBuilder.DropIndex(
                name: "IX_LicenseSealedSources_LicenseInventoryId",
                table: "LicenseSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanId",
                table: "SafetyResponsibleOfficersProfile");

            migrationBuilder.DropColumn(
                name: "AmanId",
                table: "PermitUnSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanInsertedOn",
                table: "PermitUnSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanPermitInventoryId",
                table: "PermitUnSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanId",
                table: "PermitSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanInsertedOn",
                table: "PermitSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanPermitInventoryId",
                table: "PermitSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanId",
                table: "PermitNuclearSources");

            migrationBuilder.DropColumn(
                name: "AmanInsertedOn",
                table: "PermitNuclearSources");

            migrationBuilder.DropColumn(
                name: "AmanPermitInventoryId",
                table: "PermitNuclearSources");

            migrationBuilder.DropColumn(
                name: "AmanId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "AmanInsertedOn",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "AmanLicenseMasterId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "AmanPermitMasterId",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "AmanId",
                table: "PermitGenerators");

            migrationBuilder.DropColumn(
                name: "AmanInsertedOn",
                table: "PermitGenerators");

            migrationBuilder.DropColumn(
                name: "AmanPermitInventoryId",
                table: "PermitGenerators");

            migrationBuilder.DropColumn(
                name: "AmanFacilityId",
                table: "PermitDetailsProfile");

            migrationBuilder.DropColumn(
                name: "AmanId",
                table: "PermitDetailsProfile");

            migrationBuilder.DropColumn(
                name: "AmanLicenseId",
                table: "PermitDetailsProfile");

            migrationBuilder.DropColumn(
                name: "AmanId",
                table: "LicenseVUnsealedSources");

            migrationBuilder.DropColumn(
                name: "AmanInsertedOn",
                table: "LicenseVUnsealedSources");

            migrationBuilder.DropColumn(
                name: "AmanLicenseInventoryId",
                table: "LicenseVUnsealedSources");

            migrationBuilder.DropColumn(
                name: "AmanId",
                table: "LicenseUnSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanInsertedOn",
                table: "LicenseUnSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanLicenseInventoryId",
                table: "LicenseUnSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanId",
                table: "LicenseSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanInsertedOn",
                table: "LicenseSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanLicenseInventoryId",
                table: "LicenseSealedSources");

            migrationBuilder.DropColumn(
                name: "AmanEntityId",
                table: "LicenseProfile");

            migrationBuilder.DropColumn(
                name: "AmanFacilityId",
                table: "LicenseProfile");

            migrationBuilder.DropColumn(
                name: "AmanId",
                table: "LicenseProfile");

            migrationBuilder.DropColumn(
                name: "AmanId",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropColumn(
                name: "AmanInsertedOn",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropColumn(
                name: "AmanLicenseMasterId",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropColumn(
                name: "AmanId",
                table: "LicenseGenerators");

            migrationBuilder.DropColumn(
                name: "AmanInsertedOn",
                table: "LicenseGenerators");

            migrationBuilder.DropColumn(
                name: "AmanLicenseInventoryId",
                table: "LicenseGenerators");

            migrationBuilder.DropColumn(
                name: "AmanId",
                table: "LegalRepresentativesProfile");

            migrationBuilder.DropColumn(
                name: "AmanId",
                table: "FacilityProfile");

            migrationBuilder.DropColumn(
                name: "AmanId",
                table: "EntityProfile");

            migrationBuilder.RenameColumn(
                name: "AmanId",
                table: "LookupSetTerm",
                newName: "AmanOrgId");

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "WorkersExposuresDoses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "WorkersDosimeters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "Workers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "TrnItemSourceRadionuclides",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "TrnItemSourceHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "TrnItemSource",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "TrnItemFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "TransactionTypeMaster",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "TransactionHeader",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "ServiceItemProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "ServiceItemPrice",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "SafetyResponsibleOfficersProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "RelatedItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "RelatedItemHierarchyStructure",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "RelatedItemFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "Radionuclides",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "RadiationGeneratorsProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "PractiseProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "PermitUnSealedSources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "PermitSealedSources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "PermitNuclearSources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "PermitInventoryLimits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacilityId",
                table: "PermitInventoryLimits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "PermitGenerators",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "PermitDetailsProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "NuclearRelatedItemsProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "NuclearMaterials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "NuclearMaterialRadionulcides",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "NuclearMaterialFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "ManufacturerMaster",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "LookupSet",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "ListOfValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "LicenseVUnsealedSources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "LicenseUnSealedSources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "LicenseSealedSources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LicenseInventoryLimitsLicenseInventoryId",
                table: "LicenseSealedSources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "LicenseProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "LicenseInventoryLimits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacilityId",
                table: "LicenseInventoryLimits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "PurposeOfUse",
                table: "LicenseGenerators",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "LicenseGenerators",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "LegalRepresentativesProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "ItemSourceStatusHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "ItemSourceStatus",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "ItemSourceRadionulcides",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "ItemSourceProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "ItemSourceMsgHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "ItemSourceFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "ItemHierarchyStructure",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "InternalScreens",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "InternalScreenRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "InternalScreenFields",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "InternalRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "InternalFieldPermissions",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AmanFacilityId",
                table: "FacilityProfile",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "FacilityProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "EntityProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "CustomerProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "BillingServiceTrnHeader",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "BasCountries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "BasCities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkersExposuresDoses_AmanOrgId",
                table: "WorkersExposuresDoses",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkersDosimeters_AmanOrgId",
                table: "WorkersDosimeters",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_AmanOrgId",
                table: "Workers",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSourceRadionuclides_AmanOrgId",
                table: "TrnItemSourceRadionuclides",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSourceHistory_AmanOrgId",
                table: "TrnItemSourceHistory",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_AmanOrgId",
                table: "TrnItemSource",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemFiles_AmanOrgId",
                table: "TrnItemFiles",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTypeMaster_AmanOrgId",
                table: "TransactionTypeMaster",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHeader_AmanOrgId",
                table: "TransactionHeader",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItemProfile_AmanOrgId",
                table: "ServiceItemProfile",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItemPrice_AmanOrgId",
                table: "ServiceItemPrice",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyResponsibleOfficersProfile_AmanOrgId",
                table: "SafetyResponsibleOfficersProfile",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItems_AmanOrgId",
                table: "RelatedItems",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItemHierarchyStructure_AmanOrgId",
                table: "RelatedItemHierarchyStructure",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItemFiles_AmanOrgId",
                table: "RelatedItemFiles",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Radionuclides_AmanOrgId",
                table: "Radionuclides",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RadiationGeneratorsProfile_AmanOrgId",
                table: "RadiationGeneratorsProfile",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PractiseProfile_AmanOrgId",
                table: "PractiseProfile",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PermitUnSealedSources_AmanOrgId",
                table: "PermitUnSealedSources",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PermitSealedSources_AmanOrgId",
                table: "PermitSealedSources",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PermitNuclearSources_AmanOrgId",
                table: "PermitNuclearSources",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PermitInventoryLimits_AmanOrgId",
                table: "PermitInventoryLimits",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PermitInventoryLimits_FacilityId",
                table: "PermitInventoryLimits",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitGenerators_AmanOrgId",
                table: "PermitGenerators",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PermitDetailsProfile_AmanOrgId",
                table: "PermitDetailsProfile",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearRelatedItemsProfile_AmanOrgId",
                table: "NuclearRelatedItemsProfile",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_AmanOrgId",
                table: "NuclearMaterials",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterialRadionulcides_AmanOrgId",
                table: "NuclearMaterialRadionulcides",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterialFiles_AmanOrgId",
                table: "NuclearMaterialFiles",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturerMaster_AmanOrgId",
                table: "ManufacturerMaster",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LookupSetTerm_AmanOrgId",
                table: "LookupSetTerm",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LookupSetTerm_LookupSetId_AmanOrgId",
                table: "LookupSetTerm",
                columns: new[] { "LookupSetId", "AmanOrgId" },
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LookupSet_AmanOrgId",
                table: "LookupSet",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ListOfValues_AmanOrgId",
                table: "ListOfValues",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseVUnsealedSources_AmanOrgId",
                table: "LicenseVUnsealedSources",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseUnSealedSources_AmanOrgId",
                table: "LicenseUnSealedSources",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

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
                name: "IX_LicenseProfile_AmanOrgId",
                table: "LicenseProfile",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseInventoryLimits_AmanOrgId",
                table: "LicenseInventoryLimits",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseInventoryLimits_FacilityId",
                table: "LicenseInventoryLimits",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseGenerators_AmanOrgId",
                table: "LicenseGenerators",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LegalRepresentativesProfile_AmanOrgId",
                table: "LegalRepresentativesProfile",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceStatusHistory_AmanOrgId",
                table: "ItemSourceStatusHistory",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceStatus_AmanOrgId",
                table: "ItemSourceStatus",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceRadionulcides_AmanOrgId",
                table: "ItemSourceRadionulcides",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_AmanOrgId",
                table: "ItemSourceProfile",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceMsgHistory_AmanOrgId",
                table: "ItemSourceMsgHistory",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceFiles_AmanOrgId",
                table: "ItemSourceFiles",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemHierarchyStructure_AmanOrgId",
                table: "ItemHierarchyStructure",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InternalScreens_AmanOrgId",
                table: "InternalScreens",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InternalScreenRoles_AmanOrgId",
                table: "InternalScreenRoles",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InternalScreenFields_AmanOrgId",
                table: "InternalScreenFields",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InternalRoles_AmanOrgId",
                table: "InternalRoles",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFieldPermissions_AmanOrgId",
                table: "InternalFieldPermissions",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityProfile_AmanOrgId",
                table: "FacilityProfile",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EntityProfile_AmanOrgId",
                table: "EntityProfile",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfile_AmanOrgId",
                table: "CustomerProfile",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BillingServiceTrnHeader_AmanOrgId",
                table: "BillingServiceTrnHeader",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BasCountries_AmanOrgId",
                table: "BasCountries",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BasCities_AmanOrgId",
                table: "BasCities",
                column: "AmanOrgId",
                unique: true,
                filter: "[AmanOrgId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseInventoryLimits_FacilityProfile_FacilityId",
                table: "LicenseInventoryLimits",
                column: "FacilityId",
                principalTable: "FacilityProfile",
                principalColumn: "FacilityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseSealedSources_LicenseInventoryLimits_LicenseInventoryLimitsLicenseInventoryId",
                table: "LicenseSealedSources",
                column: "LicenseInventoryLimitsLicenseInventoryId",
                principalTable: "LicenseInventoryLimits",
                principalColumn: "LicenseInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermitInventoryLimits_FacilityProfile_FacilityId",
                table: "PermitInventoryLimits",
                column: "FacilityId",
                principalTable: "FacilityProfile",
                principalColumn: "FacilityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
