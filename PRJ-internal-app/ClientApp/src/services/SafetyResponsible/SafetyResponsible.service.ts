import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from '../../helper/helper';
@Injectable({
  providedIn: 'root',
})
export class SafetyResponsibleServices {
  constructor(private helper: Helper) { }


  GetAll() {
    return this.helper.GetRequest('api/SafetyResponsibleOfficers/GetAll', null);
  }
  Add(data: any) {
    return this.helper.AddRequest('api/SafetyResponsibleOfficers/Add', data);
  }
  Update(data: any) {
    return this.helper.AddRequest('api/SafetyResponsibleOfficers/Update', data);
  }

  Delete(id: any) {
    return this.helper.GetRequest('api/SafetyResponsibleOfficers/Delete?id=' + id);
  }

}
