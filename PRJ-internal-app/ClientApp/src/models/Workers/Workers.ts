import { DatePipe } from "@angular/common";
import BaseModel from  "../Common/BaseModel" ;

export interface DTOWorkers extends BaseModel {
  id: string;
  WorkerNameAr: number;
  WorkerNameEn: number;
  BirthDate: string;
  JobPosition: string;
  Facility: string;
  NationalityId: string;
  Status: string;
  PassportNo: string;
  Nationality: string;
}
