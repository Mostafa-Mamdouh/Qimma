import BaseModel from  "../Common/BaseModel" ;

export interface LookupSetTerm extends BaseModel {

    lookupSetTermId: number;
    lookupSetId: number;
    value: string;
    displayNameAr: string;
    displayNameEn: string;
    extraData1: string;
    extraData2: string;
    isActive: boolean;

}
