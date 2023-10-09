import { FieldType } from "../../Enumerations/Enums";
import BaseModel from  "../Common/BaseModel" ;

export interface ScreenField extends BaseModel {

  fieldId: string;
  fieldDescAr: string;
  fieldDescEn: string;
  fieldType: FieldType;
  fieldTypeEn: string;
  fieldTypeAr: string;
  fieldFormat: string;
  screenId: number;
  screenNameAr: string;
  screenNameEn: string;
  lookupSetId: number;
  className: string;
  lovId: number;
  lovNameAr: string;
  lovNameEn: string;
}

export interface FieldTypeList  {
  FieldType: number;
  FieldTypeNameAr: string;
  FieldTypeNameEn: string;
}

export class FieldObjectEmmiter {
  fieldPermissionId: string;
  isLov: boolean;
  fieldId: number;
  lookupId: string;
}
 
