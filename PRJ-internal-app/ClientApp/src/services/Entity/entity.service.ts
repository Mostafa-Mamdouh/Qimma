import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from '../../helper/helper';
@Injectable({
  providedIn: 'root',
})
export class EntityServices {
  constructor(private helper: Helper) { }


  GetAll() {
    return this.helper.GetRequest('api/Entity/GetAll', null);
  }
  Add(data: any) {
    return this.helper.AddRequest('api/Entity/Add', data);
  }
  Update(data: any) {
    return this.helper.AddRequest('api/Entity/Update', data);
  }

  Delete(id: any) {
    return this.helper.GetRequest('api/Entity/Delete?id=' + id);
  }
  GetAllFunctional(body: any) {
    return this.helper.GetRequestWithBody('api/Entity/GetAllFuncational', body, null);
  }
}
