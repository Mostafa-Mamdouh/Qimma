import { Data } from "@angular/router";
import BaseModel from "../Common/BaseModel";

export interface DTOLegalRepProfile extends BaseModel {
  LegalRepresentativesNameAr: string;
  LegalRepresentativesNameEn: string;
  Title: string;
  NationalID: string;
  PhoneNo: string;
  MobileNo: string;
  EmailId: string;
  Status: string;
  CurrentFacilities: string;
  Note: string;
  AmanID: string;
  AmanInsertedOn: string;
  SourceType: string;
}
