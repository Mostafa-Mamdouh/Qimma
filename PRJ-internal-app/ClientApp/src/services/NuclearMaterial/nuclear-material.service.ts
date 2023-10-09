import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {  Helper, ParameterClass } from '../../helper/helper';
@Injectable({
  providedIn: 'root',
})
export class NuclearMaterialServices {
  constructor(private helper: Helper) { }
  getLicensesNumbers() {
    return this.helper.GetRequest('api/License/GetLicenseNumber', null);
  }

  getEntities() {
    return this.helper.GetRequest('api/Entity/GetAll', null);
  }

  getFacilities(entityId) {
    let parameter = new ParameterClass('Id', entityId);
    return this.helper.GetRequest('api/Facility/GetByEntityId',  [
      parameter,
    ]);
  }

  getLicenses(facilityId) {
    let parameter = new ParameterClass('Id', facilityId);
    return this.helper.GetRequest('api/License/GetByFacilityId',  [
      parameter,
    ]);
  }

  getPermits(licenseId) {
    let parameter = new ParameterClass('Id', licenseId);
    return this.helper.GetRequest(
      'api/PermitDetailsProfile/GetByLicenseId',
      [parameter]
    );
  }

  getRadionulicdes() {
    return this.helper.GetRequest('api/Radionuclide/GetAll', null);
  }

  getRadionulicdesFunctional(body: any) {
    return this.helper.GetRequestWithBody(
      'api/Radionuclide/GetAllFuncational',
      body,
      null
    );
  }

  getRadionulicdesByNuclearMaterial(nm: string) {
    let parameter = new ParameterClass('nuclearmaterial', nm);
    return this.helper.GetRequest(
      'api/Radionuclide/GetByNuclearMaterial',
      [parameter]
    );
  }

  getSourceById(id: string) {
    let parameter = new ParameterClass('Id', id);
    return this.helper.GetRequest('api/ItemSources/GetById', [
      parameter,
    ]);
  }

  addNuclearMaterial(body: any) {
    return this.helper.AddRequest('api/NuclearMaterial/AddNuclearMaterial', body);
  }

  updateNuclearMaterial(body: any) {
    return this.helper.AddRequest('api/NuclearMaterial/UpdateNuclearMaterial', body);
  }

  deleteNuclearMaterial(id: string) {
    let parameter = new ParameterClass('id', id);

    return this.helper.GetRequest('api/NuclearMaterial/DeleteNuclearMaterial', [
      parameter,
    ]);
  }

  getNuclearMaterialById(id: string) { 
    let parameter = new ParameterClass('id', id);
    return this.helper.GetRequest('api/NuclearMaterial/GetNuclearMaterialById', [
      parameter,
    ]);
  }

  getNuclearMaterials() {
    return this.helper.GetRequest('api/NuclearMaterial/GetNuclearMaterials', null);
  }

  getNuclearMaterialsFunctional(body: any) {
    return this.helper.GetRequestWithBody('api/NuclearMaterial/GetNuclearMaterialsFunctional', body ,null);
  }
}

