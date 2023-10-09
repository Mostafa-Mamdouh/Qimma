import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from '../../helper/helper';
@Injectable({
  providedIn: 'root',
})
export class ScreenService {
  constructor(private helper: Helper) { }
  
  GetAllScreens() {
    return this.helper.GetRequest('api/InternalScreen/GetAll', null);
  }
  AddScreen(data: any) {
    return this.helper.AddRequest('api/InternalScreen/Add', data);
  }
  UpdateScreen(data: any) {
    return this.helper.AddRequest('api/InternalScreen/Update', data);
  }
  DeleteScreen(id: any) {
    return this.helper.GetRequest('api/InternalScreen/delete?id=' + id);
  }
  GetAllScreensFunctional(body: any) {
    return this.helper.GetRequestWithBody('api/InternalScreen/GetAllFuncational', body, null);
  }
}
