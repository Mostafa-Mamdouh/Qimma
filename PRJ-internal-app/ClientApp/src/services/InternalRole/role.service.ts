import { Injectable } from '@angular/core';
import { Helper } from '../../helper/helper';
@Injectable({
  providedIn: 'root',
})
export class RoleService {
  constructor(private helper: Helper) { }
  
  GetAllRoles() {
    return this.helper.GetRequest('api/InternalRole/GetAll', null);
  }
  AddRole(data: any) {
    return this.helper.AddRequest('api/InternalRole/Add', data);
  }
  UpdateRole(data: any) {
    return this.helper.AddRequest('api/InternalRole/Update', data);
  }
  DeleteRole(id: any) {
    return this.helper.GetRequest('api/InternalRole/delete?id=' + id);
  }
  GetAllRolesFunctional(body: any) {
    return this.helper.GetRequestWithBody('api/InternalRole/GetAllFuncational', body, null);
  }
  GetAllScreens(id: any) {
    return this.helper.GetRequest('api/InternalScreenRole/GetAllScreens?id=' + id);
  }
  AddRoleScreens(body: any) {
    return this.helper.GetRequestWithBody('api/InternalScreenRole/Update', body, null);
  }
}
