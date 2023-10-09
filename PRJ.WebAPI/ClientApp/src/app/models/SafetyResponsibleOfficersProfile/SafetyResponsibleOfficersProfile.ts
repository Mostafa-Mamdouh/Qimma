import BaseModel from  "../Common/BaseModel" ;

export interface SafetyResponsibleOfficersProfile extends BaseModel {

    SRONameAr: string;
    SRONameEn: string;
    NationalID: string;
    PhoneNo: string;
    MobileNo: string;
    EmailId: string;
    CertificateNo: string;
    IssuanceDate: Date;
    ExpiryDate:Date;
    AmanInsertedOn:Date;

}