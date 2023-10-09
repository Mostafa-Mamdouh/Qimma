import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { LicenseProfile } from 'src/app/models/LicenseProfile/LicenseProfile';

@Injectable({
  providedIn: 'root',
})
export class AddWorkerService {
  constructor(private helper: Helper) {}

  GetAll() {
    return this.helper.GetRequest('api/Workers/GetAll', null);
  }
  Add(data: any) {
    return this.helper.AddRequest('api/Workers/Add', data);
  }
  Update(data: any) {
    return this.helper.AddRequest('api/Workers/Update', data);
  }
  Delete(id: any) {
    return this.helper.GetRequest('api/Workers/Delete?id=' + id);
  }
  GetAllNationality() {
    return this.helper.GetRequest('api/Common/GetCountries', null);
  }

  GetAllFunctional(body: any) {
    return this.helper.GetRequestWithBody('api/Workers/GetAllFuncational', body);
  }
}
