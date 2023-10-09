import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { LookupSet } from 'src/app/models/LookupSet/LookupSet';

@Injectable({
  providedIn: 'root',
})
export class LookupSetService {
  constructor(private helper: Helper) {}

  getAll() {
    return this.helper.GetRequest('api/LookupSet/GetAll', null);
  }

  getLookupSetById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest('api/LookupSet/GetLookupSetById', params);
  }

  addLookupSet(data: LookupSet) {
    return this.helper.AddRequest('api/LookupSet/AddLookupSet', data, null);
  }

  updateLookupSet(data: LookupSet) {
    return this.helper.PutRequest('api/LookupSet/updateLookupSet', data, null);
  }
}
