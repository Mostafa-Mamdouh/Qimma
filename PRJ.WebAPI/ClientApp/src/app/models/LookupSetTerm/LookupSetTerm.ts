import BaseModel from  "../Common/BaseModel" ;

export interface LookupSetTerm extends BaseModel {

    LookupSetTermId: number;
    LookupSetId: number;
    Value: string;
    DisplayNameAr: string;
    DisplayNameEn: string;
    ExtraData1: string;
    ExtraData2: string;
    IsActive: boolean;

}