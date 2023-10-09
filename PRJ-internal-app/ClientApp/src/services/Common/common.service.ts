import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from '../../helper/helper';
import { LookupSet } from '../../models/LookupSet/LookupSet';
@Injectable({
  providedIn: 'root',
})
export class CommonServices {
  constructor(private helper: Helper) {}

  // Cites Start
  getCitesByCountryId(Id: string) {
    return this.helper.GetRequest('api/Common/GetCitesByCountryId?id=' + Id);
  }
  getCitesByCountryIdClear(Id: number) {
    return this.helper.GetRequest(
      'api/Common/GetCitesByCountryIdClear?id=' + Id
    );
  }
  addCity(data: any) {
    return this.helper.AddRequest('api/Common/AddCity', data);
  }
  updateCity(data: any) {
    return this.helper.AddRequest('api/Common/UpdateCity', data);
  }
  deleteCity(id: string) {
    return this.helper.GetRequest('api/Common/DeleteCity?id=' + id);
  }
  getAllCitiesFunctional(data: any) {
    return this.helper.GetRequestWithBody(
      'api/Common/GetAllCitiesFuncational',
      data,
      null
    );
  }
  // Cites End

  // Countries Start
  getCountryById(Id: string) {
    return this.helper.GetRequest('api/Common/GetCountryById?Id=' + Id);
  }
  getCountryByIdClear(Id: number) {
    return this.helper.GetRequest('api/Common/GetCountryByIdClear?Id=' + Id);
  }
  getCountries() {
    return this.helper.GetRequest('api/Common/GetCountries', null);
  }
  getCountriesClear() {
    return this.helper.GetRequest('api/Common/GetCountriesClear', null);
  }
  AddCountry(data: any) {
    return this.helper.AddRequest('api/Common/AddCountries', data);
  }
  UpdateCountry(data: any) {
    return this.helper.AddRequest('api/Common/UpdateCountries', data);
  }
  DeleteCountry(id: string) {
    return this.helper.GetRequest('api/Common/DeleteCountries?id=' + id);
  }
  GetAllCountriesFunctional(body: any) {
    return this.helper.GetRequestWithBody(
      'api/Common/GetAllFuncational',
      body,
      null
    );
  }
  // Countries End

  // Lookups start
  getLookups() {
    return this.helper.GetRequest('api/LookupSet/GetAll', null);
  }
  getLookupById(Id: string) {
    return this.helper.GetRequest('api/LookupSet/GetById?Id=' + Id, null);
  }
  addLookupSet(data: any) {
    return this.helper.AddRequest('api/LookupSet/Add', data);
  }
  updateLookupSet(data: any) {
    return this.helper.AddRequest('api/LookupSet/Update', data);
  }
  getLookupByClassName(className: string) {
    return this.helper.GetRequest(
      'api/LookupSet/GetLookupsByClassName?ClassName=' + className,
      null
    );
  }
  deleteLookUp(Id: string) {
    return this.helper.GetRequest('api/LookupSet/Delete?Id=' + Id, null);
  }
  GetAllFunctional(body: any) {
    return this.helper.GetRequestWithBody(
      'api/LookupSet/GetAllFuncational',
      body,
      null
    );
  }
  getAllLookups() {
    return this.helper.GetRequest('api/LookupSet/GetLookups', null);
  }

  // lookups end

  // Lookups-term start
  getLookupsBySetId(Id: string) {
    return this.helper.GetRequest(
      'api/LookupSet/GetLookupsBySetId?Id=' + Id,
      null
    );
  }
  deleteLookUpTerm(Id: string) {
    return this.helper.GetRequest('api/LookupSet/DeleteLookUp?Id=' + Id, null);
  }
  addLookupSetTerm(data: any) {
    return this.helper.AddRequest('api/LookupSet/AddLookUp', data);
  }
  updateLookupSetTerm(data: any) {
    return this.helper.AddRequest('api/LookupSet/UpdateLookUp', data);
  }
  getAllLookupsTermsFunctional(data: any) {
    return this.helper.GetRequestWithBody(
      'api/LookupSet/GetAllLookUpsTermFunctional',
      data,
      null
    );
  }

  getSourceFormsLookup() {
    return this.helper.GetRequest('api/LookupSet/GetSourceFormsLookup', null);
  }
  getRelatedItemsSetupLookup() {
    return this.helper.GetRequest(
      'api/LookupSet/GetRelatedItemsSetupLookup',
      null
    );
  }
  // Lookups-term end

  // Practise Profile Start
  getPractiseProfile() {
    return this.helper.GetRequest('api/PractiseProfile/GetAll', null);
  }
  getAllPractisesFunctional(data: any) {
    return this.helper.GetRequestWithBody(
      'api/PractiseProfile/GetAllPractisesFunctional',
      data,
      null
    );
  }
  // Practise Profile Start

  // Permit Profile Start
  getPermitProfile() {
    return this.helper.GetRequest('api/PermitDetailsProfile/GetAll', null);
  }
  // Practise Profile Start

  // License Profile Start
  getAllLicensesFunctional(data: any) {
    return this.helper.GetRequestWithBody(
      'api/License/GetAllLicensesFuncational',
      data,
      null
    );
  }
  // License Profile End

  // TransactionType

  getAllTransactionTypes() {
    return this.helper.GetRequest('api/Common/GetTransactionTypes', null);
  }

  getAllItemSourceStatus() {
    return this.helper.GetRequest('api/Common/GetItemSourceStatus', null);
  }
}
