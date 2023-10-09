import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from '../../helper/helper';
@Injectable({
  providedIn: 'root',
})
export class LegalRepProfileServices {
  constructor(private helper: Helper) { }


  GetAll() {
    return this.helper.GetRequest('api/LegalRepProfile/GetAll', null);
  }
  Add(data: any) {
    return this.helper.AddRequest('api/LegalRepProfile/Add', data);
  }
  Update(data: any) {
    return this.helper.AddRequest('api/LegalRepProfile/Update', data);
  }

  Delete(id: any) {
    return this.helper.GetRequest('api/LegalRepProfile/Delete?id=' + id);
  }

}
