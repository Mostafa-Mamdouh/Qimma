import BaseModel from  "../Common/BaseModel" ;
import { InternalRoleScreen } from "../InternalRole/InternalRoleScreen";

export interface ScreenMaster extends BaseModel {

  screenId: number;
  screenNameAr: string;
  screenNameEn: string;
  screenCode: string;

}
