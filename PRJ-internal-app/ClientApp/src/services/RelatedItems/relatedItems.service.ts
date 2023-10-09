import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from '../../helper/helper';
import { LookupSet } from '../../models/LookupSet/LookupSet';

@Injectable({
  providedIn: 'root',
})
export class RelatedItemsService {
  constructor(private helper: Helper) {}

  GetAllHierarchy(data: string) {
    return this.helper.GetRequest(
      'api/RelatedItemsHierarchy/GetAll?itemStr=' + data
    );
  }
  GetAsLookup() {
    return this.helper.GetRequest('api/RelatedItemsHierarchy/GetAsLookup');
  }
  AddHierarchy(data: any) {
    return this.helper.AddRequest('api/RelatedItemsHierarchy/Add', data);
  }
  UpdateHierarchy(data: any) {
    return this.helper.AddRequest('api/RelatedItemsHierarchy/Update', data);
  }
  DeleteHierarchy(code: string) {
    return this.helper.GetRequest(
      'api/RelatedItemsHierarchy/Delete?code=' + code
    );
  }
  GetHierarchyByCode(code: string) {
    return this.helper.GetRequest(
      'api/RelatedItemsHierarchy/GetByCode?code=' + code
    );
  }
  GetAllHierarchyFunctional(code: any) {
    return this.helper.GetRequestWithBody(
      'api/RelatedItems/GetFunctionalwithStructureCode',
      code
    );
  }
  GetFunctionalwithStructureCode(code: any, value: string) {
    return this.helper.GetRequestWithBody(
      'api/RelatedItems/GetFunctionalwithStructureCode',
      [code, value]
    );
  }
  GetAll() {
    return this.helper.GetRequest('api/RelatedItems/GetAll', null);
  }
  Add(data: any) {
    return this.helper.AddRequest('api/RelatedItems/Add', data);
  }
  Update(data: any) {
    return this.helper.AddRequest('api/RelatedItems/Update', data);
  }
  GetById(id: any) {
    return this.helper.GetRequest(
      'api/RelatedItems/GetById?relatedItemCode=' + id
    );
  }
  GetAllFunctional(body: any) {
    return this.helper.GetRequestWithBody(
      'api/RelatedItems/GetAllFuncational',
      body,
      null
    );
  }
  DeleteById(id: any) {
    return this.helper.GetRequest(
      'api/RelatedItems/Delete?relatedItemCode=' + id
    );
  }
  //1h3ma
}
