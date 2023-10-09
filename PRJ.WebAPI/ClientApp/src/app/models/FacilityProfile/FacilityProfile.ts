import BaseModel from  "../Common/BaseModel" ;
import { EntityProfile } from "../EntityProfile/EntityProfile";

export interface FacilityProfile extends BaseModel {

    FacilityNameAr: string;
    FacilityNameEn: string;
    OragnizationName: string;
    FacilityCode: string;
    Province: string;
    City: string;
    Location: string;
    EntityId: number;
    EntityProfile: EntityProfile;
    AmanInsertedOn:Date;


}