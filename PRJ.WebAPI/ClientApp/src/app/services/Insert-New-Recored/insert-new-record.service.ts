import { Injectable } from '@angular/core';
import { param } from 'jquery';
import { Helper, ParameterClass } from 'src/app/helper/Helper';

@Injectable({
  providedIn: 'root',
})
export class InsertNewRecordService {
  constructor(private helper: Helper) {}

  getAllLookups() {
    return this.helper.GetRequest('api/LookupSet/GetLookups', null);
  }

  getSourceFormsLookup() {
    return this.helper.GetRequest('api/LookupSet/GetSourceFormsLookup', null);
  }

  getLookupByClassName(type: string) {
    let parameter = new ParameterClass('ClassName', type);
    return this.helper.GetRequest('api/LookupSet/GetLookupsByClassName', null, [
      parameter,
    ]);
  }

  getLicensesNumbers() {
    return this.helper.GetRequest('api/LicenseProfile/GetLicenseNumber', null);
  }

  getEntities() {
    return this.helper.GetRequest('api/EntityProfile/GetAll', null);
  }

  getFacilities(entityId) {
    let parameter = new ParameterClass('Id', entityId);
    return this.helper.GetRequest('api/FacilityProfile/GetByEntityId', null, [
      parameter,
    ]);
  }

  getLicenses(facilityId) {
    console.log('facilityId', facilityId);
    let parameter = new ParameterClass('Id', facilityId);
    return this.helper.GetRequest('api/LicenseProfile/GetByFacilityId', null, [
      parameter,
    ]);
  }

  getPermits(licenseId) {
    let parameter = new ParameterClass('Id', licenseId);
    return this.helper.GetRequest(
      'api/PermitDetailsProfile/GetByLicenseId',
      null,
      [parameter]
    );
  }

  getRadionulicdes() {
    return this.helper.GetRequest('api/Radionuclide/GetAll', null);
  }

  getRadionulicdesFunctional(body: any) {
    return this.helper.GetRequest(
      'api/Radionuclide/GetAllFuncational',
      body,
      null
    );
  }

  getRadionulicdesByNuclearMaterial(nm: string) {
    let parameter = new ParameterClass('nuclearmaterial', nm);
    return this.helper.GetRequest(
      'api/Radionuclide/GetByNuclearMaterial',
      null,
      [parameter]
    );
  }

  getSourceById(id: string) {
    let parameter = new ParameterClass('Id', id);
    return this.helper.GetRequest('api/ItemSources/GetById', null, [parameter]);
  }
}
