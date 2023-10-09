import { Data } from "@angular/router";
import BaseModel from "../Common/BaseModel";

export interface DTOLicense extends BaseModel {

  
  LicenseDescAr: string;
  LicenseDescEn: string;
  LicenseCode: string;
  LicenseVersionNumber: string;
  EffectiveDate: string;
  ExpirationDate: string;
  practiseSector: string;
  LicenseActivities: string;
  EntityId: string;
  FacilityId: string;
  AmanInsertedOn: string;
  AmanID: string;
 
}
