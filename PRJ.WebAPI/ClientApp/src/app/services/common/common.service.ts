import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';

@Injectable({
  providedIn: 'root',
})
export class CommonService {
  constructor(private helper: Helper) {}

  getCountries() {
    return this.helper.GetRequest('api/Common/GetCountries', null);
  }

  getCountryById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest('api/Common/GetCountryById', params);
  }

  getNationalities() {
    return this.helper.GetRequest('api/Common/GetNationalites', null);
  }

  getNationalityById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest('api/Common/GetNationalityById', params);
  }

  getCountryByCode(code: string) {
    var params: ParameterClass[] = [new ParameterClass('code', code)];
    return this.helper.GetRequest('api/Common/GetCountryByCode', params);
  }

  getNationalityByCode(code: string) {
    var params: ParameterClass[] = [new ParameterClass('code', code)];
    return this.helper.GetRequest('api/Common/GetNationalityByCode', params);
  }

  getCityByCountryCode(code: string) {
    var params: ParameterClass[] = [new ParameterClass('code', code)];
    return this.helper.GetRequest('api/Common/GetCityByCountryCode', params);
  }

  getCityById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest('api/Common/GetCityById', params);
  }

  getLookupByClassName(className: string) {
    return this.helper.GetRequest('api/LookupSet/GetLookupsByClassName?ClassName=' + className, null);
  }
  
}
