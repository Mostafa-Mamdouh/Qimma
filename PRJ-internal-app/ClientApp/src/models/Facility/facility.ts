import BaseModel from "../Common/BaseModel";

export interface DTOFacility extends BaseModel {

  

  FacilityNameAr: string;
  FacilityNameEn: string;
  OragnizationName: string;
  FacilityCode: string;
  Province: string;
  City: string;
  Location: string;
  EntityId: Date | null;
  AmanInsertedOn: string;
  AmanID: string;
  FacilitySource: string;
}
