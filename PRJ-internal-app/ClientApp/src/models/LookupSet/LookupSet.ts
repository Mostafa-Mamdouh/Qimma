import BaseModel from  "../Common/BaseModel" ;

export interface LookupSet extends BaseModel {

  lookupSetId: number;
  parentId: number;
  className: string;
  displayNameAr: string;
  displayNameEn: string;
  extraData1: string;
  extraData2: string;
  isActive: boolean;

}
