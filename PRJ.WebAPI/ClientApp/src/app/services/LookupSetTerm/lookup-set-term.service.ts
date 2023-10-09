import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { LookupSetTerm } from './../../models/LookupSetTerm/LookupSetTerm';

@Injectable({
  providedIn: 'root',
})
export class LookupSetTermService {
  constructor(private helper: Helper) {}

  getAll() {
    return this.helper.GetRequest('api/LookupSetTerm/GetAll', null);
  }

  getLookupSetTermById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest(
      'api/LookupSetTerm/GetLookupSetTermById',
      params
    );
  }

  addLookupSetTerm(data: LookupSetTerm) {
    return this.helper.AddRequest(
      'api/LookupSetTerm/AddLookupSetTerm',
      data,
      null
    );
  }

  updateLookupSetTerm(data: LookupSetTerm) {
    return this.helper.PutRequest(
      'api/LookupSetTerm/updateLookupSetTerm',
      data,
      null
    );
  }
}
