import { Contries } from '../BaseContries/BasContries';
import BaseModel from '../Common/BaseModel';
import { EntityProfile } from '../EntityProfile/EntityProfile';
import { FacilityProfile } from '../FacilityProfile/FacilityProfile';
import { LegalRepresentativesProfile } from '../LegalRepresentativesProfile/LegalRepresentativesProfile';
import { LicenseInventoryLimits } from '../LicenseInventoryLimits/LicenseInventoryLimits';
import { LicenseProfile } from '../LicenseProfile/LicenseProfile';
import { LookupSet } from '../LookupSet/LookupSet';
import { ManufacturerMaster } from '../ManufacturerMaster/ManufacturerMaster';
import { PermitDetailsProfile } from '../PermitDetailsProfile/PermitDetailsProfile';
import { PermitInventoryLimits } from '../PermitInventoryLimits/PermitInventoryLimits';
import { PractiseProfile } from '../PractiseProfile/PractiseProfile';
import { SafetyResponsibleOfficersProfile } from '../SafetyResponsibleOfficersProfile/SafetyResponsibleOfficersProfile';

export interface ItemSourcesProfile extends BaseModel {
  SourceId: number;
  SourceDescAr: string;
  SourceDescEn: string;
  NrrcID: string;
  Status: string; //existed in lookup
  ManufacturerSerialNo: string;
  FacilitySerialNo: string;
  RadioNuclides: string; //existed in lookup
  ProductionDate: Date;
  IsoptopHalfLifeTime: number;
  IspotopHalfLifeTimeUnit: string;
  InitialActivity: string;
  InitialActivityUnit: string; //existed in lookup
  CurrentActivity: string;
  CurrentActivityUnit: string;
  PhysicalForm: string; //existed in lookup
  Qty: number;
  Unit: string;
  Exemption: string;
  RecommendedWorkingLifeTime: number; //existed in lookup
  RadioactiveSourceid: string;
  ShieldMaterial: string; //existed in lookup
  ShieldNuclearMaterialCode: string;
  Purpose: string;
  NumberOfAcquiredSources: number;
  NumberOfConsumedSources: number;
  NumberOfAvailableSources: number;
  NuclearMaterial: string;
  WeightOfNuclearMaterial: number;
  EnrichmentofU235: string;
  WeightOffIssileisotopes: string;
  WeightOffIssileisotopesunit: string; //existed in loookup
  ChemicalForm: string; //existed in lookup
  ChemicalComposition: string;
  ContainerVolume: number;
  SourceActivity: string;
  SourceModel: string;
  AssociatedEquipment: string; //existed in lookup
  SourceCategory: string;
  SourceLinkedFlag: number;
  EntityId: number;
  EntityProfile: EntityProfile;
  FacilityId: number;
  FacilityProfile: FacilityProfile;
  LicenseId: number; //existed in lookup
  LicenseProfile: LicenseProfile;
  LicenseInventoryId: number;
  LicenseInventoryLimits: LicenseInventoryLimits;
  PermitdetailsId: number; //existed in lookup
  PermitDetailsProfile: PermitDetailsProfile;
  PermitInventoryId: number;
  PermitInventoryLimits: PermitInventoryLimits;
  PractiseId: number;
  PractiseProfile: PractiseProfile;
  SROId: number;
  SafetyResponsibleOfficersProfile: SafetyResponsibleOfficersProfile;
  LegalRepresentativesId: number;
  LegalRepresentativesProfile: LegalRepresentativesProfile;
  ManufacturerId: number; //existed in lookup
  ManufacturerMaster: ManufacturerMaster;
  ManufactureCountryId: number;
  BasCountries: Contries;
  ItemTypeId: number;
  LookupSet: LookupSet;
  //sourceType: string;
}
