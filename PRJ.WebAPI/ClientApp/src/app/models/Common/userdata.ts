export default class UserData {
  username: string;
  firstNameAr: string;
  secondNameAr: string;
  lastNameAr: string;
  firstNameEn: string;
  secondNameEn: string;
  lastNameEn: string;
  nationality: string;
  nationalID: string;
  email: string;
  gregorianBirthDate: string;
  picture: string;
  passportNo: string;
  createdBy: string;
  modifiedBy: string;
  createdOn: Date;
  modifiedOn: Date;
  accessToken: string;
}




export class TokenDto {
  accessToken: string;
  refreshToken: string;
}


