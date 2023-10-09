import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { FacilityProfile } from 'src/app/models/FacilityProfile/FacilityProfile';

@Injectable({
  providedIn: 'root',
})
export class FacilityProfileService {
  constructor(private helper: Helper) {}

  getAll() {
    return this.helper.GetRequest('api/FacilityProfile/GetAll', null);
  }

  getFacilityProfileById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest(
      'api/FacilityProfile/GetFacilityProfileById',
      params
    );
  }

  getFacilityProfileByEntityId(id: string) {
    return this.helper.GetRequest(
      'api/FacilityProfile/GetByEntityId?Id='+id
    );
  }
  


  addFacilityProfile(data: FacilityProfile) {
    return this.helper.AddRequest(
      'api/FacilityProfile/AddFacilityProfile',
      data,
      null
    );
  }

  updateFacilityProfile(data: FacilityProfile) {
    return this.helper.PutRequest(
      'api/FacilityProfile/UpdateFacilityProfile',
      data,
      null
    );
  }
}
