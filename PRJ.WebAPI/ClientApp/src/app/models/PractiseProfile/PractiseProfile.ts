import BaseModel from  "../Common/BaseModel" ;
import { PermitDetailsProfile } from "../PermitDetailsProfile/PermitDetailsProfile";

export interface PractiseProfile extends BaseModel {

    PractiseId: number;
    PractiseNameAr: string;
    PractiseNameEn: string;
    PermitDetailsId: number;
    PermitDetailsProfile: PermitDetailsProfile;

}