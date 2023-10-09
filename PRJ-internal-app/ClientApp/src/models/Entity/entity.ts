import { Data } from "@angular/router";
import BaseModel from "../Common/BaseModel";

export interface DTOEntity extends BaseModel {
  EntityNameAr: string;
  EntityNameEn: string;
  PhoneNo: string;
  MobileNo: string;
  EmailId: string;
  GovernmentID: string;
  EntityType: string;
  AmanInsertedOn: Date | null;
  AmanID: string;
}
