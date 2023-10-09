import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { LicenseProfile } from 'src/app/models/LicenseProfile/LicenseProfile';

@Injectable({
  providedIn: 'root',
})
export class LicenseProfileService {
  constructor(private helper: Helper) {}

  getAll() {
    return this.helper.GetRequest('api/LicenseProfile/GetAll', null);
  }

  getLicenseProfileById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest(
      'api/LicenseProfile/GetLicenseProfileById',
      params
    );
  }

  
  getLicenseProfileByFacilityId(id: string) {
    return this.helper.GetRequest(
      'api/LicenseProfile/GetByFacilityId?Id='+id  
        );
  } 
 
  addLicenseProfile(data: LicenseProfile) {
    return this.helper.AddRequest(
      'api/LicenseProfile/AddLicenseProfile',
      data,
      null
    );
  }

  updateLicenseProfile(data: LicenseProfile) {
    return this.helper.PutRequest(
      'api/LicenseProfile/updateLicenseProfile',
      data,
      null
    );
  }
}
