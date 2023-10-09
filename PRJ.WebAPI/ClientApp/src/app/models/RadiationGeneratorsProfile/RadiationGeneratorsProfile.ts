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

export interface RadiationGeneratorsProfile extends BaseModel {

    EquipmentDescAr: string;
    EquipmentDescEn: string;
    NrrcID: string;
    Status: string;
    ManufacturerSerialNo: string;
    DateofManufacturing: Date;
    FacilitySerialNo: string;
    Purpose: string;
    ModelNumber:Date;
    XRayTubeSerialNo: string;
    MaxEnergy: string;
    EnergyUnit: string;
    MaxDoseRate: string;
    DoseUnit: string;
    MaxCurrent: string;
    Unit: string;
    SheildMaterial: string;
    ShieldNuclearMaterialCode: string;
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
    EquipmentType: number;
    LookupSet: LookupSet;




}