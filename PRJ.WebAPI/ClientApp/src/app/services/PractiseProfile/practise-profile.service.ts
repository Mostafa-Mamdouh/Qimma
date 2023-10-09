import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { PractiseProfile } from 'src/app/models/PractiseProfile/PractiseProfile';

@Injectable({
  providedIn: 'root',
})
export class PractiseProfileService {
  constructor(private helper: Helper) {}

  getAll() {
    return this.helper.GetRequest('api/PractiseProfile/GetAll', null);
  }

  getPractiseProfileById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest(
      'api/PractiseProfile/GetPractiseProfileById',
      params
    );
  }

  addPractiseProfile(data: PractiseProfile) {
    return this.helper.AddRequest(
      'api/PractiseProfile/AddPractiseProfile',
      data,
      null
    );
  }

  getPractiseProfileByLicenseId(id: string){
    return this.helper.GetRequest(
      'api/PractiseProfile/GetPractiseByLicenseId?id='+id);
  }

  updatePractiseProfile(data: PractiseProfile) {
    return this.helper.PutRequest(
      'api/PractiseProfile/updatePractiseProfile',
      data,
      null
    );
  }
}
