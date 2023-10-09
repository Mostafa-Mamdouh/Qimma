import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from '../../helper/helper';
@Injectable({
  providedIn: 'root',
})
export class ScreenFieldService {
  constructor(private helper: Helper) { }
  
  GetAllScreenFields() {
    return this.helper.GetRequest('api/InternalScreenField/GetAll', null);
  }
  GetByScreenFieldsId(id: any) {
    return this.helper.GetRequest('api/InternalScreenField/GetById?id=' + id, null);
  }
  AddScreenFields(data: any) {
    return this.helper.AddRequest('api/InternalScreenField/Add', data);
  }
  UpdateScreenFields(data: any) {
    return this.helper.AddRequest('api/InternalScreenField/Update', data);
  }
  DeleteScreenFields(id: any) {
    return this.helper.GetRequest('api/InternalScreenField/delete?id=' + id);
  }
  GetAllScreenFieldsFunctional(body: any) {
    return this.helper.GetRequestWithBody('api/InternalScreenField/GetAllFuncational', body, null);
  }
  SaveFieldPermissions(body: any) {
    console.log(body)
    return this.helper.GetRequestWithBody('api/InternalScreenField/save-field-permission', body, null);
  }
  GetFieldPermissionsById(id: any,roleId:any) {
    return this.helper.GetRequest('api/InternalScreenField/GetByFieldId?id=' + id + '&roleId=' + roleId, null);
  }
}
