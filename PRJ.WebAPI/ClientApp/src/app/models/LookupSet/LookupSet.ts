import BaseModel from  "../Common/BaseModel" ;

export interface LookupSet extends BaseModel {

    LookupSetId: number;
    ParentId: number;
    ClassName: string;
    DisplayNameAr: string;
    DisplayNameEn: string;
    ExtraData1: Date;
    ExtraData2: Date;
    IsActive: boolean;

}