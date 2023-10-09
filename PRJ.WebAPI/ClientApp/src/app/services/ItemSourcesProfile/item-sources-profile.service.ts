import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { AddNewSourceModel } from 'src/app/models/addNewSourceModel/addNewSourceModel';
import { ItemSourcesProfile } from 'src/app/models/ItemSourcesProfile/ItemSourcesProfile ';

@Injectable({
  providedIn: 'root',
})
export class ItemSourcesProfileService {
  constructor(private helper: Helper) {}

  getAll() {
    return this.helper.GetRequest('api/ItemSourcesProfile/GetAll', null);
  }

  getItemSourcesProfileById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest(
      'api/ItemSourcesProfile/GetItemSourcesProfileById',
      params
    );
  }

  getTrnItemSourceById(id: number) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest('api/ItemSources/GetById', null, params);
  }

  addItemSourcesProfile(data: ItemSourcesProfile) {
    return this.helper.AddRequest(
      'api/ItemSourcesProfile/AddItemSourcesProfile',
      data,
      null
    );
  }

  saveItemSource(data: AddNewSourceModel) {
    return this.helper.AddRequest(
      'api/ItemSources/SaveTransactionItemSource',
      data,
      null
    );
  }
}
