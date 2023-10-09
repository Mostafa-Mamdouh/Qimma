using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRJ.DataAccess.Migrations
{
    public partial class OrgIDColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "WorkersExposuresDoses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "WorkersDosimeters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "Workers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "TrnItemSourceRadionuclides",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "TrnItemSourceHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "TrnItemSource",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "TrnItemFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "TransactionTypeMaster",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "TransactionHeader",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "ServiceItemProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "ServiceItemPrice",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "SafetyResponsibleOfficersProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "RelatedItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "RelatedItemHierarchyStructure",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "RelatedItemFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "Radionuclides",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "RadiationGeneratorsProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "PractiseProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "PermitInventoryLimits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "NuclearRelatedItemsProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "NuclearMaterials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "NuclearMaterialRadionulcides",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "NuclearMaterialFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "ManufacturerMaster",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "LookupSetTerm",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "LookupSet",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "ListOfValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "LicenseInventoryLimits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "LegalRepresentativesProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "ItemSourceStatusHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "ItemSourceStatus",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "ItemSourceRadionulcides",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "ItemSourceProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "ItemSourceMsgHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "ItemSourceFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "ItemHierarchyStructure",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "InternalScreens",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "InternalScreenRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "InternalScreenFields",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "InternalRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "InternalFieldPermissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "FacilityProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "EntityProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "CustomerProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "BillingServiceTrnHeader",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "BasCountries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrgID",
                table: "BasCities",
                type: "int",
                nullable: true);


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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "WorkersExposuresDoses");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "WorkersDosimeters");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "TrnItemSourceRadionuclides");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "TrnItemSourceHistory");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "TrnItemSource");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "TrnItemFiles");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "TransactionTypeMaster");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "TransactionHeader");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "ServiceItemProfile");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "ServiceItemPrice");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "SafetyResponsibleOfficersProfile");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "RelatedItems");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "RelatedItemHierarchyStructure");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "RelatedItemFiles");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "Radionuclides");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "RadiationGeneratorsProfile");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "PractiseProfile");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "PermitInventoryLimits");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "NuclearRelatedItemsProfile");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "NuclearMaterials");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "NuclearMaterialRadionulcides");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "NuclearMaterialFiles");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "ManufacturerMaster");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "LookupSetTerm");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "LookupSet");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "ListOfValues");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "LicenseInventoryLimits");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "LegalRepresentativesProfile");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "ItemSourceStatusHistory");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "ItemSourceStatus");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "ItemSourceRadionulcides");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "ItemSourceProfile");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "ItemSourceMsgHistory");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "ItemSourceFiles");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "ItemHierarchyStructure");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "InternalScreens");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "InternalScreenRoles");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "InternalScreenFields");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "InternalRoles");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "InternalFieldPermissions");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "FacilityProfile");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "EntityProfile");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "CustomerProfile");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "BillingServiceTrnHeader");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "BasCountries");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "BasCities");
        }
    }
}
