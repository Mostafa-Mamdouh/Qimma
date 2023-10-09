import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Helper, ParameterClass } from 'src/app/helper/Helper';

@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  constructor(private helper: Helper) {}

  GetServiceItemByHierarchyCode(body: string): Observable<any> {
    return this.helper.GetRequest(
      'api/BillingServiceTrnHeader/GetServiceItemByHierarchyCode?Code=' + body
    );
  }

  getAllTransactionsFunctional(body: any): Observable<any> {
    return this.helper.GetRequest(
      'api/ItemSources/GetLandPaginData',
      body,
      null
    );
  }

  GetFacility(): Observable<any> {
    return this.helper.GetRequest('api/CustomerProfile/GetAll');
  }
  GetStatus(): Observable<any> {
    return this.helper.GetRequest(
      'api/LookupSet/GetLookupsByClassName?ClassName=TrnStatus'
    );
  }
  GetPaymentTerm(): Observable<any> {
    return this.helper.GetRequest(
      'api/LookupSet/GetLookupsByClassName?ClassName=PaymentTerm'
    );
  }
  GetAllSector(): Observable<any> {
    return this.helper.GetRequest('api/ItemHierarchyStructure/GetLevelTwo');
  }
  GetAllPractice(parent: any): Observable<any> {
    return this.helper.GetRequest(
      'api/ItemHierarchyStructure/GetLevelThreeByParent?parent=' + parent
    );
  }
  GetAllFunction(body: any): Observable<any> {
    return this.helper.GetRequest(
      'api/BillingServiceTrnHeader/GetAllFuncational',
      body
    );
  }

  GetParentItemStructureCode(body: any): Observable<any> {
    return this.helper.GetRequest(
      'api/ItemHierarchyStructure/GetByCode?code=' + body
    );
  }

  getOldInvoice(id: string): Observable<any> {
    return this.helper.GetRequest(
      'api/BillingServiceTrnHeader/GetWithId?id=' + id
    );
  }
  SaveData(body: any): Observable<any> {
    return this.helper.AddRequest('api/BillingServiceTrnHeader/Add', body);
  }
  Update(body: any): Observable<any> {
    return this.helper.AddRequest('api/BillingServiceTrnHeader/Update', body);
  }
  GetVat(): Observable<any> {
    return this.helper.GetRequest(
      'api/LookupSet/GetLookupsByClassName?ClassName=SysParams'
    );
  }

  DeleteDraft(id: number) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest('api/ItemSources/DeleteDraft', null, params);
  }
  CreateSimiler(id: number) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest(
      'api/ItemSources/CreateSimiler',
      null,
      params
    );
  }
  GetSourcesByPermit(permitId: string, sourceType: number) {
    var params: ParameterClass[] = [
      new ParameterClass('permitId', permitId),
      new ParameterClass('sourceType', sourceType),
    ];
    return this.helper.GetRequest(
      'api/ItemSources/GetSourcesByPermit',
      null,
      params
    );
  }
  editSubmittedTransaction(body: any) {
    return this.helper.AddRequest('api/ItemSources/EditItemSource', body, null);
  }
}
