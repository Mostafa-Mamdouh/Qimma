import BaseModel from  "../Common/BaseModel" ;
import { EntityProfile } from "../EntityProfile/EntityProfile";
import { LicenseProfile } from "../LicenseProfile/LicenseProfile";

export interface LicenseInventoryLimits extends BaseModel {

    LicenseInventoryId: number;
    SourceType: string;
    Radionuclide: string;
    MaximumRadioactivity: number;
    NumberofSources: number;
    LicenseId: number;
    LicenseProfile: LicenseProfile;
    AmanInsertedOn:Date;


}