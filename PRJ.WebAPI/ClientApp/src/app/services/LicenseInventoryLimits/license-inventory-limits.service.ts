import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { LicenseInventoryLimits } from 'src/app/models/LicenseInventoryLimits/LicenseInventoryLimits';

@Injectable({
  providedIn: 'root',
})
export class LicenseInventoryLimitsService {
  constructor(private helper: Helper) {}

  getAll() {
    return this.helper.GetRequest('api/LicenseInventoryLimits/GetAll', null);
  }

  getLicenseInventoryLimitById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest(
      'api/LicenseInventoryLimits/GetLicenseInventoryLimitsById',
      params
    );
  }

  addLicenseInventoryLimits(data: LicenseInventoryLimits) {
    return this.helper.AddRequest(
      'api/LicenseInventoryLimits/AddLicenseInventoryLimits',
      data,
      null
    );
  }

  updateLicenseInventoryLimits(data: LicenseInventoryLimits) {
    return this.helper.PutRequest(
      'api/LicenseInventoryLimits/updateLicenseInventoryLimits',
      data,
      null
    );
  }
}
