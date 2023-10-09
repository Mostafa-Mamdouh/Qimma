import BaseModel from  "../Common/BaseModel" ;

export interface InternalRoleScreen extends BaseModel {

  screenRoleId: number | null;
  screenNameAr: string;
  screenNameEn: string;
  roleNameAr: string;
  roleNameEn: string;
  screenId: number;
  roleId: number | null;
  roleIdValue: string;
  screenOrder: number | null;
  insert: boolean;
  update: boolean;
  delete: boolean;
  query: boolean;
}

