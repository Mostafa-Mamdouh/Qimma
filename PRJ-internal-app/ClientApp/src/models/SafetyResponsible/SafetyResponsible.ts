import BaseModel from "../Common/BaseModel";

export interface DTOSafetyResponsible extends BaseModel {

  SRONameAr: string;
  SRONameEn: string;
  Title: string;
  NationalID: string;
  PhoneNo: string;
  MobileNo: string;
  EmailId: string;
  Status: string;
  AmanInsertedOn: string;
  AmanID: string;
  CertificateNo: string;
  IssuanceDate: string;
  ExpiryDate: string;
  SourceType: string;

}
