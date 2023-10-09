import { Injectable } from '@angular/core';
import { Helper } from '../../helper/helper';
@Injectable({
  providedIn: 'root',
})
export class LovService {
  constructor(private helper: Helper) { }
  
  GetAllLov() {
    return this.helper.GetRequest('api/ListOfValue/GetAll', null);
  }
  AddLov(data: any) {
    return this.helper.AddRequest('api/ListOfValue/Add', data);
  }
  UpdateLov(data: any) {
    return this.helper.AddRequest('api/ListOfValue/Update', data);
  }
  DeleteLov(id: any) {
    return this.helper.GetRequest('api/ListOfValue/delete?id=' + id);
  }
  GetAllLovFunctional(body: any) {
    return this.helper.GetRequestWithBody('api/ListOfValue/GetAllFuncational', body, null);
  }
}
