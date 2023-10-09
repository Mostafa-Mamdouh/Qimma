import BaseModel from  "../Common/BaseModel" ;
import { EntityProfile } from "../EntityProfile/EntityProfile";
import { FacilityProfile } from "../FacilityProfile/FacilityProfile";

export interface LicenseProfile extends BaseModel {

    LicenseId: number;
    LicenseDescAr: string;
    LicenseDescEn: string;
    LicenseCode: string;
    LicenseVersionNumber: string;
    EffectiveDate: Date;
    ExpirationDate: Date;
    PractiseSector: string;
    LicenseActivities: string;
    EntityId: number;
    EntityProfile: EntityProfile;
    FacilityId: number;
    FacilityProfile: FacilityProfile;
    AmanInsertedOn:Date;


}