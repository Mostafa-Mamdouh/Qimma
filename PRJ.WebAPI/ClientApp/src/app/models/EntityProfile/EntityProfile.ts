import BaseModel from "../Common/BaseModel";

export interface EntityProfile extends BaseModel {

    EntityNameAr: string;
    EntityNameEn: string;
    PhoneNo: string;
    MobileNo: string;
    EmailId: string;
    GovernmentID: string;
    EntityType: string;
    AmanInsertedOn: Date;


}