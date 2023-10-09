import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from '../../helper/helper';
@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  constructor(private helper: Helper) { }

  
  getAllTransactionsFunctional(body: any): Observable<any> {
    return this.helper.GetRequest(
      'api/ItemSources/GetLandPaginData',
      body
    );
  }

  GetFacility(): Observable<any> {
    return this.helper.GetRequest('api/CustomerProfile/GetAll');
  }
  GetStatus(): Observable<any> {
    return this.helper.GetRequest('api/LookupSet/GetLookupsByClassName?ClassName=TrnStatus');
  }
  GetPaymentTerm(): Observable<any> {
    return this.helper.GetRequest('api/LookupSet/GetLookupsByClassName?ClassName=PaymentTerm');
  }
  GetAllSector(): Observable<any> {
    return this.helper.GetRequest('api/ItemHierarchyStructure/GetLevelTwo');
  }
  GetAllPractice(parent: any): Observable<any> {
    return this.helper.GetRequest('api/ItemHierarchyStructure/GetLevelThreeByParent?parent=' + parent);
  }
  GetServiceItemByHierarchyCode(body: string): Observable<any> {
    return this.helper.GetRequest('api/BillingServiceTrnHeader/GetServiceItemByHierarchyCode?Code=' + body)
  }

  GetAllFunction(param: any): Observable<any> {
    return this.helper.AddRequest('api/BillingServiceTrnHeader/GetAllFuncational', param);
  }

  GetParentItemStructureCode(body: any): Observable<any> {
    return this.helper.GetRequest('api/ItemHierarchyStructure/GetByCode?code=' + body);
  }

  getOldInvoice(id: string): Observable<any> {
    return this.helper.GetRequest('api/BillingServiceTrnHeader/GetWithId?id=' + id);
  }
  SaveData(body: any): Observable<any> {
    return this.helper.AddRequest('api/BillingServiceTrnHeader/Add', body);
  }
  Update(body: any): Observable<any> {
    return this.helper.AddRequest('api/BillingServiceTrnHeader/Update', body);
  }
  GetVat(): Observable<any> {
    return this.helper.GetRequest('api/LookupSet/GetLookupsByClassName?ClassName=SysParams');
  }

  DeleteDraft(id: number) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequestWithBody('api/ItemSources/DeleteDraft', null, params);
  }
  editSubmittedTransaction(body: any) {
    return this.helper.AddRequest('api/ItemSources/EditItemSource', body, null);
  }
}
