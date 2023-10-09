import BaseModel from  "../Common/BaseModel" ;

export interface DTOManufacturerMaster extends BaseModel {

  ManufacturerDescAr: string;
  ManufacturerDescEn: string;
  PhoneNo: string;
  MobileNo: string;
  EmailId: string;
  Location: string;
  AddressLine1: string;
  AddressLine2: string;
  AddressLine3: string;
  City: number;
  ZipCode: string;
  POBox: string;
  CountryId: number;
}
