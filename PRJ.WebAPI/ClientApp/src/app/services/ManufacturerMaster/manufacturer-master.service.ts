import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { ManufacturerMaster } from 'src/app/models/ManufacturerMaster/ManufacturerMaster';

@Injectable({
  providedIn: 'root',
})
export class ManufacturerMasterService {
  constructor(private helper: Helper) {}

  getAll() {
    return this.helper.GetRequest('api/ManufacturerMaster/GetAll', null);
  }

  getManufacturerById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest(
      'api/ManufacturerMaster/GetManufacturerMasterById',
      params
    );
  }

  addManufacturer(data: ManufacturerMaster) {
    return this.helper.AddRequest(
      'api/ManufacturerMaster/AddManufacturerMaster',
      data,
      null
    );
  }

  updateManufacturer(data: ManufacturerMaster) {
    return this.helper.PutRequest(
      'api/ManufacturerMaster/UpdateManufacturerMaster',
      data,
      null
    );
  }
}
