import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { PermitInventoryLimits } from 'src/app/models/PermitInventoryLimits/PermitInventoryLimits';

@Injectable({
  providedIn: 'root',
})
export class PermitInventoryLimitsService {
  constructor(private helper: Helper) {}

  getAll() {
    return this.helper.GetRequest('api/PermitInventoryLimits/GetAll', null);
  }

  getPermitInventoryLimitsById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest(
      'api/PermitInventoryLimits/GetPermitInventoryLimitsById',
      params
    );
  }

  addPermitInventoryLimits(data: PermitInventoryLimits) {
    return this.helper.AddRequest(
      'api/PermitInventoryLimits/AddPermitInventoryLimits',
      data,
      null
    );
  }

  updatePermitInventoryLimits(data: PermitInventoryLimits) {
    return this.helper.PutRequest(
      'api/PermitInventoryLimits/updatePermitInventoryLimits',
      data,
      null
    );
  }
}
