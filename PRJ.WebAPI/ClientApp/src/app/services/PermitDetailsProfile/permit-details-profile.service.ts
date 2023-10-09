import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { PermitDetailsProfile } from 'src/app/models/PermitDetailsProfile/PermitDetailsProfile';

@Injectable({
  providedIn: 'root',
})
export class PermitDetailsProfileService {
  constructor(private helper: Helper) {}

  getAll() {
    return this.helper.GetRequest('api/PermitDetailsProfile/GetAll', null);
  }

  getPermitDetailsProfileById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest(
      'api/PermitDetailsProfile/GetPermitDetailsProfileById',
      params
    );
  }

  addPermitDetailsProfile(data: PermitDetailsProfile) {
    return this.helper.AddRequest(
      'api/PermitDetailsProfile/AddPermitDetailsProfile',
      data,
      null
    );
  }

  updatePermitDetailsProfile(data: PermitDetailsProfile) {
    return this.helper.PutRequest(
      'api/PermitDetailsProfile/updatePermitDetailsProfile',
      data,
      null
    );
  }
}
