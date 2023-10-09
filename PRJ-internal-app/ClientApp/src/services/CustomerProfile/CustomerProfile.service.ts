import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from '../../helper/helper';
@Injectable({
  providedIn: 'root',
})
export class CustomerProfileService {
  constructor(private helper: Helper) { }

  GetAll() {
    return this.helper.GetRequest('api/CustomerProfile/GetAll', null);
  }
  Add(data: any) {
    return this.helper.AddRequest('api/CustomerProfile/Add', data);
  }
  Update(data: any) {
    return this.helper.AddRequest('api/CustomerProfile/Update', data);
  }
  GetById(id: any) {
    return this.helper.GetRequest('api/CustomerProfile/GetById?customerId' + id);
  }
  GetAllFunctional(body: any) {
    return this.helper.GetRequestWithBody('api/CustomerProfile/GetAllFuncational', body, null);
  }
  DeleteById(id: any) {
    return this.helper.AddRequest('api/CustomerProfile/Delete' , id);
  }
}


