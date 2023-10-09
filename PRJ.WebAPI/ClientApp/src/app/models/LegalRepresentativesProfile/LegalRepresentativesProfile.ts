import BaseModel from  "../Common/BaseModel" ;

export interface LegalRepresentativesProfile extends BaseModel {

    LegalRepresentativesId: number;
    LegalRepresentativesNameAr: string;
    LegalRepresentativesNameEn: string;
    Title: string;
    NationalID: string;
    PhoneNo: string;
    MobileNo: string;
    EmailId: string;
    Status: string;
    CurrentFacilities: number;
    Note: string;
    AmanInsertedOn:Date;

}