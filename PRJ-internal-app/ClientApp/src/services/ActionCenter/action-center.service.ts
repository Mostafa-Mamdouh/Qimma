import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from '../../helper/helper';
@Injectable({
  providedIn: 'root',
})
export class ActionCenterService {
  constructor(private helper: Helper) { }
  
  
  GetAllActionsFunctional(body: any) {
    return this.helper.GetRequestWithBody('api/ActionCenter/GetAllFuncational', body, null);
  }
  GetAllEnquiryFunctional(body: any) {
    return this.helper.GetRequestWithBody('api/ActionCenter/GetAllEnquiryFuncational', body, null);
  }
  AddNewStatusHistory(body: any) {
    return this.helper.GetRequestWithBody('api/ActionCenter/AddNewStatusHistory', body, null);
  }

  SendEmail(body: any) {
    return this.helper.GetRequestWithBody('api/ActionCenter/SendMail', body, null);
  }

  GetEditHistory(id: string, trnType: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id), new ParameterClass('trnType', trnType)];
    return this.helper.GetRequest('api/TrnItemSource/GetEditHistory', params);
  }

  GetSourceById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest('api/TrnItemSource/GetById', params);
  }
  GetProfileById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest('api/ActionCenter/GetById', params);
  }

}
