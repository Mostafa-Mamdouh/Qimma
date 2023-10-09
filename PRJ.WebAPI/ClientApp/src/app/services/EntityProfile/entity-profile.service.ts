import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { EntityProfile } from 'src/app/models/EntityProfile/EntityProfile';

@Injectable({
  providedIn: 'root',
})
export class EntityProfileService {
  constructor(private helper: Helper) {}

  getAll() {
    return this.helper.GetRequest('api/EntityProfile/GetAll', null);
  }

  getEntityProfileById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest(
      'api/EntityProfile/GetEntityProfileById',
      params
    );
  }

  addEntity(data: EntityProfile) {
    return this.helper.AddRequest(
      'api/EntityProfile/AddEntityProfile',
      data,
      null
    );
  }

  updateEntity(data: EntityProfile) {
    return this.helper.PutRequest(
      'api/EntityProfile/UpdateEntityProfile',
      data,
      null
    );
  }
}
