import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from '../../helper/helper';
import { LookupSet } from '../../models/LookupSet/LookupSet';

@Injectable({
  providedIn: 'root',
})
export class BillingService {
  constructor(private helper: Helper) {}

  GetAll() {
    return this.helper.GetRequest('api/ItemHierarchyStructure/GetAll');
  }
  Add(data: any) {
    return this.helper.AddRequest('api/ItemHierarchyStructure/Add', data);
  }
  Update(data: any) {
    return this.helper.AddRequest('api/ItemHierarchyStructure/Update', data);
  }
  Delete(code: string) {
    return this.helper.GetRequest(
      'api/ItemHierarchyStructure/Delete?code=' + code
    );
  }
  GetByCode(code: string) {
    return this.helper.GetRequest(
      'api/ItemHierarchyStructure/GetByCode?code=' + code
    );
  }
  GetServicesByItemCode(data: any) {
    return this.helper.GetRequestWithBody('api/ServiceItem/GetAll', data);
  }
}
