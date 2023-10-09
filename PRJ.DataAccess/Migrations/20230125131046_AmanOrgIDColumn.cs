using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class AmanOrgIDColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkersExposuresDoses_OrgID",
                table: "WorkersExposuresDoses");

            migrationBuilder.DropIndex(
                name: "IX_WorkersDosimeters_OrgID",
                table: "WorkersDosimeters");

            migrationBuilder.DropIndex(
                name: "IX_Workers_OrgID",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_TrnItemSourceRadionuclides_OrgID",
                table: "TrnItemSourceRadionuclides");

            migrationBuilder.DropIndex(
                name: "IX_TrnItemSourceHistory_OrgID",
                table: "TrnItemSourceHistory");

            migrationBuilder.DropIndex(
                name: "IX_TrnItemSource_OrgID",
                table: "TrnItemSource");

            migrationBuilder.DropIndex(
                name: "IX_TrnItemFiles_OrgID",
                table: "TrnItemFiles");

            migrationBuilder.DropIndex(
                name: "IX_TransactionTypeMaster_OrgID",
                table: "TransactionTypeMaster");

            migrationBuilder.DropIndex(
                name: "IX_TransactionHeader_OrgID",
                table: "TransactionHeader");

            migrationBuilder.DropIndex(
                name: "IX_ServiceItemProfile_OrgID",
                table: "ServiceItemProfile");

            migrationBuilder.DropIndex(
                name: "IX_ServiceItemPrice_OrgID",
                table: "ServiceItemPrice");

            migrationBuilder.DropIndex(
                name: "IX_SafetyResponsibleOfficersProfile_OrgID",
                table: "SafetyResponsibleOfficersProfile");

            migrationBuilder.DropIndex(
                name: "IX_RelatedItems_OrgID",
                table: "RelatedItems");

            migrationBuilder.DropIndex(
                name: "IX_RelatedItemHierarchyStructure_OrgID",
                table: "RelatedItemHierarchyStructure");

            migrationBuilder.DropIndex(
                name: "IX_RelatedItemFiles_OrgID",
                table: "RelatedItemFiles");

            migrationBuilder.DropIndex(
                name: "IX_Radionuclides_OrgID",
                table: "Radionuclides");

            migrationBuilder.DropIndex(
                name: "IX_RadiationGeneratorsProfile_OrgID",
                table: "RadiationGeneratorsProfile");

            migrationBuilder.DropIndex(
                name: "IX_PractiseProfile_OrgID",
                table: "PractiseProfile");

            migrationBuilder.DropIndex(
                name: "IX_PermitInventoryLimits_OrgID",
                table: "PermitInventoryLimits");

            migrationBuilder.DropIndex(
                name: "IX_NuclearRelatedItemsProfile_OrgID",
                table: "NuclearRelatedItemsProfile");

            migrationBuilder.DropIndex(
                name: "IX_NuclearMaterials_OrgID",
                table: "NuclearMaterials");

            migrationBuilder.DropIndex(
                name: "IX_NuclearMaterialRadionulcides_OrgID",
                table: "NuclearMaterialRadionulcides");

            migrationBuilder.DropIndex(
                name: "IX_NuclearMaterialFiles_OrgID",
                table: "NuclearMaterialFiles");

            migrationBuilder.DropIndex(
                name: "IX_ManufacturerMaster_OrgID",
                table: "ManufacturerMaster");

            migrationBuilder.DropIndex(
                name: "IX_LookupSetTerm_OrgID",
                table: "LookupSetTerm");

            migrationBuilder.DropIndex(
                name: "IX_LookupSet_OrgID",
                table: "LookupSet");

            migrationBuilder.DropIndex(
                name: "IX_ListOfValues_OrgID",
                table: "ListOfValues");

            migrationBuilder.DropIndex(
                name: "IX_LicenseInventoryLimits_OrgID",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropIndex(
                name: "IX_LegalRepresentativesProfile_OrgID",
                table: "LegalRepresentativesProfile");

            migrationBuilder.DropIndex(
                name: "IX_ItemSourceStatusHistory_OrgID",
                table: "ItemSourceStatusHistory");

            migrationBuilder.DropIndex(
                name: "IX_ItemSourceStatus_OrgID",
                table: "ItemSourceStatus");

            migrationBuilder.DropIndex(
                name: "IX_ItemSourceRadionulcides_OrgID",
                table: "ItemSourceRadionulcides");

            migrationBuilder.DropIndex(
                name: "IX_ItemSourceProfile_OrgID",
                table: "ItemSourceProfile");

            migrationBuilder.DropIndex(
                name: "IX_ItemSourceMsgHistory_OrgID",
                table: "ItemSourceMsgHistory");

            migrationBuilder.DropIndex(
                name: "IX_ItemSourceFiles_OrgID",
                table: "ItemSourceFiles");

            migrationBuilder.DropIndex(
                name: "IX_ItemHierarchyStructure_OrgID",
                table: "ItemHierarchyStructure");

            migrationBuilder.DropIndex(
                name: "IX_InternalScreens_OrgID",
                table: "InternalScreens");

            migrationBuilder.DropIndex(
                name: "IX_InternalScreenRoles_OrgID",
                table: "InternalScreenRoles");

            migrationBuilder.DropIndex(
                name: "IX_InternalScreenFields_OrgID",
                table: "InternalScreenFields");

            migrationBuilder.DropIndex(
                name: "IX_InternalRoles_OrgID",
                table: "InternalRoles");

            migrationBuilder.DropIndex(
                name: "IX_InternalFieldPermissions_OrgID",
                table: "InternalFieldPermissions");

            migrationBuilder.DropIndex(
                name: "IX_FacilityProfile_OrgID",
                table: "FacilityProfile");

            migrationBuilder.DropIndex(
                name: "IX_EntityProfile_OrgID",
                table: "EntityProfile");

            migrationBuilder.DropIndex(
                name: "IX_CustomerProfile_OrgID",
                table: "CustomerProfile");

            migrationBuilder.DropIndex(
                name: "IX_BillingServiceTrnHeader_OrgID",
                table: "BillingServiceTrnHeader");

            migrationBuilder.DropIndex(
                name: "IX_BasCountries_OrgID",
                table: "BasCountries");

            migrationBuilder.DropIndex(
                name: "IX_BasCities_OrgID",
                table: "BasCities");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "WorkersExposuresDoses",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "WorkersDosimeters",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "Workers",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "TrnItemSourceRadionuclides",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "TrnItemSourceHistory",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "TrnItemSource",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "TrnItemFiles",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "TransactionTypeMaster",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "TransactionHeader",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "ServiceItemProfile",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "ServiceItemPrice",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "SafetyResponsibleOfficersProfile",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "RelatedItems",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "RelatedItemHierarchyStructure",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "RelatedItemFiles",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "Radionuclides",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "RadiationGeneratorsProfile",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "PractiseProfile",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "PermitInventoryLimits",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "NuclearRelatedItemsProfile",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "NuclearMaterials",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "NuclearMaterialRadionulcides",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "NuclearMaterialFiles",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "ManufacturerMaster",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "LookupSetTerm",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "LookupSet",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "ListOfValues",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "LicenseInventoryLimits",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "LegalRepresentativesProfile",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "ItemSourceStatusHistory",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "ItemSourceStatus",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "ItemSourceRadionulcides",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "ItemSourceProfile",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "ItemSourceMsgHistory",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "ItemSourceFiles",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "ItemHierarchyStructure",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "InternalScreens",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "InternalScreenRoles",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "InternalScreenFields",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "InternalRoles",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "InternalFieldPermissions",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "FacilityProfile",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "EntityProfile",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "CustomerProfile",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "BillingServiceTrnHeader",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "BasCountries",
                newName: "AmanOrgId");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "BasCities",
                newName: "AmanOrgId");

            migrationBuilder.AddColumn<int>(
                name: "AmanOrgId",
                table: "LicenseProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LicenseProfile",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "LicenseProfile",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "LicenseProfile",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "LicenseProfile",
                type: "datetime2",
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
                name: "IX_PermitInventoryLimits_AmanOrgId",
                table: "PermitInventoryLimits",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_PermitInventoryLimits_AmanOrgId",
                table: "PermitInventoryLimits");

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
                name: "IX_LookupSetTerm_AmanOrgId",
                table: "LookupSetTerm");

            migrationBuilder.DropIndex(
                name: "IX_LookupSet_AmanOrgId",
                table: "LookupSet");

            migrationBuilder.DropIndex(
                name: "IX_ListOfValues_AmanOrgId",
                table: "ListOfValues");

            migrationBuilder.DropIndex(
                name: "IX_LicenseProfile_AmanOrgId",
                table: "LicenseProfile");

            migrationBuilder.DropIndex(
                name: "IX_LicenseInventoryLimits_AmanOrgId",
                table: "LicenseInventoryLimits");

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
                table: "LicenseProfile");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LicenseProfile");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "LicenseProfile");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "LicenseProfile");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "LicenseProfile");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "WorkersExposuresDoses",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "WorkersDosimeters",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "Workers",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "TrnItemSourceRadionuclides",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "TrnItemSourceHistory",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "TrnItemSource",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "TrnItemFiles",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "TransactionTypeMaster",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "TransactionHeader",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "ServiceItemProfile",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "ServiceItemPrice",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "SafetyResponsibleOfficersProfile",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "RelatedItems",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "RelatedItemHierarchyStructure",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "RelatedItemFiles",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "Radionuclides",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "RadiationGeneratorsProfile",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "PractiseProfile",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "PermitInventoryLimits",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "NuclearRelatedItemsProfile",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "NuclearMaterials",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "NuclearMaterialRadionulcides",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "NuclearMaterialFiles",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "ManufacturerMaster",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "LookupSetTerm",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "LookupSet",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "ListOfValues",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "LicenseInventoryLimits",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "LegalRepresentativesProfile",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "ItemSourceStatusHistory",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "ItemSourceStatus",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "ItemSourceRadionulcides",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "ItemSourceProfile",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "ItemSourceMsgHistory",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "ItemSourceFiles",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "ItemHierarchyStructure",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "InternalScreens",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "InternalScreenRoles",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "InternalScreenFields",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "InternalRoles",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "InternalFieldPermissions",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "FacilityProfile",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "EntityProfile",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "CustomerProfile",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "BillingServiceTrnHeader",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "BasCountries",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AmanOrgId",
                table: "BasCities",
                newName: "OrgID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkersExposuresDoses_OrgID",
                table: "WorkersExposuresDoses",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkersDosimeters_OrgID",
                table: "WorkersDosimeters",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_OrgID",
                table: "Workers",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSourceRadionuclides_OrgID",
                table: "TrnItemSourceRadionuclides",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSourceHistory_OrgID",
                table: "TrnItemSourceHistory",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemSource_OrgID",
                table: "TrnItemSource",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrnItemFiles_OrgID",
                table: "TrnItemFiles",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTypeMaster_OrgID",
                table: "TransactionTypeMaster",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHeader_OrgID",
                table: "TransactionHeader",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItemProfile_OrgID",
                table: "ServiceItemProfile",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItemPrice_OrgID",
                table: "ServiceItemPrice",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyResponsibleOfficersProfile_OrgID",
                table: "SafetyResponsibleOfficersProfile",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItems_OrgID",
                table: "RelatedItems",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItemHierarchyStructure_OrgID",
                table: "RelatedItemHierarchyStructure",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItemFiles_OrgID",
                table: "RelatedItemFiles",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Radionuclides_OrgID",
                table: "Radionuclides",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RadiationGeneratorsProfile_OrgID",
                table: "RadiationGeneratorsProfile",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PractiseProfile_OrgID",
                table: "PractiseProfile",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PermitInventoryLimits_OrgID",
                table: "PermitInventoryLimits",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearRelatedItemsProfile_OrgID",
                table: "NuclearRelatedItemsProfile",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterials_OrgID",
                table: "NuclearMaterials",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterialRadionulcides_OrgID",
                table: "NuclearMaterialRadionulcides",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NuclearMaterialFiles_OrgID",
                table: "NuclearMaterialFiles",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturerMaster_OrgID",
                table: "ManufacturerMaster",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LookupSetTerm_OrgID",
                table: "LookupSetTerm",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LookupSet_OrgID",
                table: "LookupSet",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ListOfValues_OrgID",
                table: "ListOfValues",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseInventoryLimits_OrgID",
                table: "LicenseInventoryLimits",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LegalRepresentativesProfile_OrgID",
                table: "LegalRepresentativesProfile",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceStatusHistory_OrgID",
                table: "ItemSourceStatusHistory",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceStatus_OrgID",
                table: "ItemSourceStatus",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceRadionulcides_OrgID",
                table: "ItemSourceRadionulcides",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceProfile_OrgID",
                table: "ItemSourceProfile",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceMsgHistory_OrgID",
                table: "ItemSourceMsgHistory",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSourceFiles_OrgID",
                table: "ItemSourceFiles",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemHierarchyStructure_OrgID",
                table: "ItemHierarchyStructure",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InternalScreens_OrgID",
                table: "InternalScreens",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InternalScreenRoles_OrgID",
                table: "InternalScreenRoles",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InternalScreenFields_OrgID",
                table: "InternalScreenFields",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InternalRoles_OrgID",
                table: "InternalRoles",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFieldPermissions_OrgID",
                table: "InternalFieldPermissions",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityProfile_OrgID",
                table: "FacilityProfile",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EntityProfile_OrgID",
                table: "EntityProfile",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfile_OrgID",
                table: "CustomerProfile",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BillingServiceTrnHeader_OrgID",
                table: "BillingServiceTrnHeader",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BasCountries_OrgID",
                table: "BasCountries",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BasCities_OrgID",
                table: "BasCities",
                column: "OrgID",
                unique: true,
                filter: "[OrgID] IS NOT NULL");
        }
    }
}
