import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from '../../helper/helper';
@Injectable({
  providedIn: 'root',
})
export class LicenseServices {
  constructor(private helper: Helper) { }


  GetAll() {
    return this.helper.GetRequest('api/License/GetAll', null);
  }
  Add(data: any) {
    return this.helper.AddRequest('api/License/Add', data);
  }
  Update(data: any) {
    return this.helper.AddRequest('api/License/Update', data);
  }

  Delete(id: any) {
    return this.helper.GetRequest('api/License/Delete?id=' + id);
  }

}
