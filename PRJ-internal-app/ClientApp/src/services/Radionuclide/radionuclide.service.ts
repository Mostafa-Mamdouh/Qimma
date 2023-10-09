import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from '../../helper/helper';
import { LookupSet } from '../../models/LookupSet/LookupSet';

@Injectable({
  providedIn: 'root',
})
export class RadionuclideService {
  constructor(private helper: Helper) { }

  GetAll() {
    return this.helper.GetRequest('api/Radionuclide/GetAll');
  }
  Add(data: any) {
    return this.helper.AddRequest('api/Radionuclide/Add', data);
  }
  Update(data: any) {
    return this.helper.AddRequest('api/Radionuclide/Update', data);
  }
  Delete(Id: string) {
    return this.helper.GetRequest('api/Radionuclide/Delete?Id=' + Id);
  }
  GetRadionulicdesFunctional(body: any) {
    return this.helper.GetRequestWithBody(
      'api/Radionuclide/GetAllFuncational',
      body,
      null
    );
  }
}
