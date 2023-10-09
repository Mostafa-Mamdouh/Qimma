import BaseModel from  "../Common/BaseModel" ;

export interface Worker extends BaseModel {
    workerNameAr: string;
    workerNameEn: string;
    passportNo: string;
    nationalityId: string;
    nationality: string;
    birthDate:Date;
    status :string;
    gender :string;
    mobileNo : string;
    jobPosition:string;
    facilityId:string;
    currentPractise : string;
    currentLicense :string;
    basCountries : {
        countryNameAr:string,
        countryNameEn:string,
    };
    facilityProfile : {
        facilityNameAr:string,
        facilityNameEn:string
    }
    lookupSetTerm :{
        displayNameAr:string,
        displayNameEn:string,
        id:string

    }
    genderLookup : { 
        displayNameAr:string,
        displayNameEn:string,
        id:string
    }
    jobPositionLookup : {
        displayNameAr:string,
        displayNameEn:string,
        id:string,

    }

}
export interface DTOWorkers extends BaseModel {
    id: string;
    WorkerNameAr: string;
    WorkerNameEn: string;
    Gender : string;
    BirthDate: string;
    JobPosition: string;
    Facility: string;
    Entity:string;
    License :string;
    Practise : string;
    NationalityId: string;
    Status: string;
    PassportNo: string;
    Nationality: string;
    currentPractise : string;
    currentLicense :string;
    mobileNo : string;
    FacilityId:string;

  }
  export interface QuarterModel {
    customerID:string;
    cmpNum:string; 
    quarterCode:string;
    quarterDescFrn:string; 
    quarterDescNtv:string;
    quarterLongDescNtv:string;
    quarterLongDescFrn:string;
  }
  export interface MassUpdateDataTable {
    workerNameEn:string;
    workerNameAr:string;
    nationalityId:string;
    genderEn:string;
    genderAr:string;
    birthDate:string;
    jobPositionAr:string;
    jobPositionEn:string;
    statusAr:string;
    statusEn:string;
    q1Value:string;
    q2Value:string;
    q3Value:string;
    q4Value:string;
    thisYear : number;
  }


  



  

  