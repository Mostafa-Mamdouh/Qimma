import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppSettings } from 'src/app/AppSettings';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { LicenseProfile } from 'src/app/models/LicenseProfile/LicenseProfile';

@Injectable({
  providedIn: 'root',
})
export class WorkerService {
  constructor(private helper: Helper,private httpclient: HttpClient) {}
  WebApiUrl: string = AppSettings.WebApiUrl;
  getAll() {
    return this.helper.GetRequest('api/Workers/GetAll', null);
  }

  getById(id: string) {
    return this.helper.GetRequest('api/Workers/GetById?Id='+id);
  }

  add(data: LicenseProfile) {
    return this.helper.AddRequest(
      'api/LicenseProfile/AddLicenseProfile',
      data,
      null
    );
  }
  getQuarter(){
    return this.helper.GetRequest(
      'api/Workers/GetAllQuarter',
    )
  }
  getWorkerFunctional(body: any) {
    return this.helper.GetRequest(
      'api/Workers/GetAllFuncational',
      body,
      null
    );
  }

  GetAllFunctionalMassUpdate(body: any) {
    return this.helper.GetRequest(
      'api/Workers/GetAllFunctionalMassUpdate',
      body,
      null
    );
  }
  
  update(data: any) {
    return this.helper.AddRequest(
      'api/Workers/Update',
      data,
    );
  }
  getLookupByClassName(className: string) {
    return this.helper.GetRequest('api/LookupSet/GetLookupsByClassName?ClassName=' + className, null);
  }

  AddDosimeters(obj : any){
    return this.helper.AddRequest('api/WorkersDosimeters/Add', obj);
  }
  GethistoryDosByUserId(id:any){
    return this.helper.GetRequest('api/WorkersDosimeters/GetById?Id='+id);
  }
  TransferWorker(obj){
    return 0;
  }
  GetFacility(){
    return this.helper.GetRequest('api/FacilityProfile/GetAll');
  }
  AddReading(obj :any ){
    return this.helper.AddRequest('api/WorkersExposuresDoses/Add' , obj);
  }
  GetLastReading(id : any){
    return this.helper.GetRequest('api/WorkersExposuresDoses/GetLastReading?Id='+id);
  }
  getEntityById(id : string){
    return this.helper.GetRequest('api/EntityProfile/GetById?Id='+id);
  };
 getLicenseById(id : string){
  return this.helper.GetRequest('api/LicenseProfile/GetById?Id='+id);
  } ;
 getFacilityById(id : string){
  return this.helper.GetRequest('api/FacilityProfile/GetById?Id='+id);
 };
 updatestatus(obj:any){
  return this.helper.AddRequest('api/Workers/UpdateStatus' , obj);
 }

 getYears(){
  return this.helper.GetRequest('api/DssFiscalYears/GetYears');
 }

 importExcel(fileInfo : FormData){
  return this.helper.AddRequestFile('api/Workers/import' , fileInfo);
}
ExportExcel(License : string) {
  console.log('2')
  return this.helper
    .GetRequestFile('api/Workers/ExportExcel?License='+License, {
      responseType: 'blob',
    })
    .pipe();
}

downlode_file(License : string,selectedYear:string , quarter:string) {
  const downlode_file_url = "api/Workers/ExportExcel?License="+License+"&selectedYear="+selectedYear+"&quarter="+quarter
   let headers = new Headers({ 
     'Content-Type':'application/json', 
     'Accept': 'application/.xls' //give file extension
  });
  let options = { headers : headers };
   return this.httpclient.post(this.WebApiUrl + downlode_file_url,options,{responseType: "blob"});
 }
 

 AddReadingFromDataTable(obj : any , year , quarter:string){
  return this.helper.AddRequest('api/WorkersExposuresDoses/AddReadingFromMassPage?year='+year+'&quarter='+quarter , obj);
 }

 GetQuarterByMonth(month : string){
  return this.helper.GetRequest('api/WorkersExposuresDoses/GetQuarterByMonth?month='+month);
 }

}

