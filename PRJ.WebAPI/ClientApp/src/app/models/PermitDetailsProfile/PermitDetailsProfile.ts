import BaseModel from  "../Common/BaseModel" ;
import { EntityProfile } from "../EntityProfile/EntityProfile";
import { FacilityProfile } from "../FacilityProfile/FacilityProfile";
import { LicenseProfile } from "../LicenseProfile/LicenseProfile";

export interface PermitDetailsProfile extends BaseModel {

    PermitDetailsId: number;
    PermitDetailsDescAr: string;
    PermitDetailsDescEn: string;
    PermitNumber: string;
    EffectiveDate: Date;
    ExpirationDate: Date;
    LicenseId: number;
    LicenseProfile: LicenseProfile;
    AmanInsertedOn:Date;




}