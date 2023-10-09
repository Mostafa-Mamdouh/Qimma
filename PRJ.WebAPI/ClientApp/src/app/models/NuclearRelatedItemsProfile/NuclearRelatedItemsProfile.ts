import { Contries } from "../BaseContries/BasContries";
import BaseModel from  "../Common/BaseModel" ;
import { EntityProfile } from "../EntityProfile/EntityProfile";
import { FacilityProfile } from "../FacilityProfile/FacilityProfile";
import { LegalRepresentativesProfile } from "../LegalRepresentativesProfile/LegalRepresentativesProfile";
import { LicenseInventoryLimits } from "../LicenseInventoryLimits/LicenseInventoryLimits";
import { LicenseProfile } from "../LicenseProfile/LicenseProfile";
import { LookupSet } from "../LookupSet/LookupSet";
import { ManufacturerMaster } from "../ManufacturerMaster/ManufacturerMaster";
import { PermitDetailsProfile } from "../PermitDetailsProfile/PermitDetailsProfile";
import { PermitInventoryLimits } from "../PermitInventoryLimits/PermitInventoryLimits";
import { PractiseProfile } from "../PractiseProfile/PractiseProfile";
import { SafetyResponsibleOfficersProfile } from "../SafetyResponsibleOfficersProfile/SafetyResponsibleOfficersProfile";



export interface NuclearRelatedItemsProfile extends BaseModel {

    NRRCRelatedItemId: number;
    RelatedItemDescAr: string;
    RelatedItemDescEn: string;
    NrrcID: string;
    Status: string;
    ManufacturerSerialNo: string;
    DateofManufacturing: Date;
    FacilityRelatedItemID: string;
    Purpose: string;
    ItemTypeNo: string;
    ItemtypeName: string;
    ModelNumber: string;
    HSCode: string;
    GovernmentCommitmentsFlag: string;
    EndUserCertificateFlag: number;
    Unit: string;
    PermitInitialQty: number;
    EntityId: number;
    EntityProfile: EntityProfile;
    FacilityId: number;
    FacilityProfile: FacilityProfile;
    LicenseId: number;
    LicenseProfile: LicenseProfile;
    LicenseInventoryId: number;
    LicenseInventoryLimits: LicenseInventoryLimits;
    PermitdetailsId: number;
    PermitDetailsProfile: PermitDetailsProfile;
    PermitInventoryId: number;
    PermitInventoryLimits: PermitInventoryLimits;
    PractiseId: number;
    PractiseProfile: PractiseProfile;
    SROId: number;
    SafetyResponsibleOfficersProfile: SafetyResponsibleOfficersProfile;
    LegalRepresentativesId: number;
    LegalRepresentativesProfile: LegalRepresentativesProfile;
    ManufacturerId: number;
    ManufacturerMaster: ManufacturerMaster;
    ManufactureCountryId: number;
    BasCountries: Contries;
    ItemCategory: number;
    LookupSet: LookupSet;

}