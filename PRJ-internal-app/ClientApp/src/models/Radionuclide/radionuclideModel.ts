import BaseModel from  "../Common/BaseModel" ;

export interface RadionuclideModel extends BaseModel {

  radionuclideId: number;
  isotop: string;
  displayName: string;
  dValue: number;
  halfLife: number;
  halfLifeUnit: string;
  initialActivity: number;
  activityUnit: string;
  exemptionValue: number;
  exemptionValueUnit: string;
  isActive: boolean;
}
