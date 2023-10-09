import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from '../../helper/helper';
@Injectable({
  providedIn: 'root',
})
export class FacilityServices {
  constructor(private helper: Helper) { }


  GetAll() {
    return this.helper.GetRequest('api/Facility/GetAll', null);
  }
  Add(data: any) {
    return this.helper.AddRequest('api/Facility/Add', data);
  }
  Update(data: any) {
    return this.helper.AddRequest('api/Facility/Update', data);
  }

  Delete(id: any) {
    return this.helper.GetRequest('api/Facility/Delete?id=' + id);
  }
  GetAllFunctional(data: any) {
    return this.helper.GetRequestWithBody('api/Facility/GetAllFunctional', data, null);
  }

}
