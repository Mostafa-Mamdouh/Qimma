import { SafeResourceUrl } from '@angular/platform-browser';
import BaseModel from './../Common/BaseModel';

export interface UserDetails extends BaseModel {
  firstNameAr: string;
  secondNameAr: string;
  lastNameAr: string;
  firstNameEn: string;
  secondNameEn: string;
  lastNameEn: string;
  nationality: string;
  nationalID: string;
  email: string;
  gregorianBirthDate: Date;
  picture: SafeResourceUrl;
  passportNo: string;
}
