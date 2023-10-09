import BaseModel from  "../Common/BaseModel" ;
import { PermitDetailsProfile } from "../PermitDetailsProfile/PermitDetailsProfile";

export interface PermitInventoryLimits extends BaseModel {

    PermitInventoryId: number;
    SourceSerialNo: string;
    ManufactureName: string;
    Radionuclide: string;
    ModelMaximumRadioactivity: string;
    Unit: string;
    PermitDetailsId: number;
    PermitDetailsProfile: PermitDetailsProfile;
    AmanInsertedOn:Date;


}