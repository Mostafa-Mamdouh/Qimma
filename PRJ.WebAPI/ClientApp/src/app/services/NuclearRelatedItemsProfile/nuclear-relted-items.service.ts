import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { NuclearRelatedItemsProfile } from 'src/app/models/NuclearRelatedItemsProfile/NuclearRelatedItemsProfile';

@Injectable({
  providedIn: 'root',
})
export class NuclearRelatedItemsService {
  constructor(private helper: Helper) {}

  getAll() {
    return this.helper.GetRequest(
      'api/NuclearRelatedItemsProfile/GetAll',
      null
    );
  }

  getNuclearRelatedItemById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest(
      'api/NuclearRelatedItemsProfile/GetNuclearRelatedItemById',
      params
    );
  }

  addNuclearRelatedItem(data: NuclearRelatedItemsProfile) {
    return this.helper.AddRequest(
      'api/NuclearRelatedItemsProfile/AddNuclearRelatedItem',
      data,
      null
    );
  }

  updateNuclearRelatedItem(data: NuclearRelatedItemsProfile) {
    return this.helper.PutRequest(
      'api/NuclearRelatedItemsProfile/UpdateNuclearRelatedItem',
      data,
      null
    );
  }
}
