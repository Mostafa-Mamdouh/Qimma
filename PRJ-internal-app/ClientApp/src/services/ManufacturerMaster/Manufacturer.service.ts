import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from '../../helper/helper';
@Injectable({
  providedIn: 'root',
})
export class ManufacturerService {
  constructor(private helper: Helper) { }

  GetAll() {
    return this.helper.GetRequest('api/ManufacturerMaster/GetAll', null);
  }
  Add(data: any) {
    return this.helper.AddRequest('api/ManufacturerMaster/Add', data);
  }
  Update(data: any) {
    return this.helper.AddRequest('api/ManufacturerMaster/Update', data);
  }
  DeleteManufacturer(id: any) {
    return this.helper.GetRequest('api/ManufacturerMaster/delete?id=' + id);
  }
  GetAllFunctional(body: any) {
    return this.helper.GetRequestWithBody('api/ManufacturerMaster/GetAllFuncational', body, null);
  }
}
