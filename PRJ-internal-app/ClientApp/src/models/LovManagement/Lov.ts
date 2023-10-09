import BaseModel from  "../Common/BaseModel" ;

export interface Lov extends BaseModel {

  lovId: number;
  lovNameAr: string;
  lovNameEn: string;
  lovCode: string;
  sqlStatement: string;
  canDelete: boolean;
}

