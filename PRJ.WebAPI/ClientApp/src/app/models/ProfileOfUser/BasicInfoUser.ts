import BaseModel from  "../Common/BaseModel" ;

export interface BasicInfoUsers extends BaseModel {

    EmployeeNo: number;
    Username: string;
    EmployeeName: string;
    EmailId: string;
    Department: string;
    JobTitle: string;
    WorkPhoneNo: string;
    MobileNo: string;
    Office: string;

}