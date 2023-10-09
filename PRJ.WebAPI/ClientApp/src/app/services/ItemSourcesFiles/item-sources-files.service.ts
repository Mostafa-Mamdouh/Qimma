import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { ItemSourcesFiles } from 'src/app/models/ItemSourcesFiles/ItemSourcesFiles';

@Injectable({
  providedIn: 'root',
})
export class ItemSourcesFilesService {
  constructor(private helper: Helper) {}

  getAll() {
    return this.helper.GetRequest('api/ItemSourcesFiles/GetAll', null);
  }

  getItemSourcesFileById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest(
      'api/ItemSourcesFiles/GetItemSourcesFilesById',
      params
    );
  }

  addItemSourcesFile(data: ItemSourcesFiles) {
    return this.helper.AddRequest(
      'api/ItemSourcesFiles/AddItemSourcesFiles',
      data,
      null
    );
  }

  updateItemSourcesFile(data: ItemSourcesFiles) {
    return this.helper.PutRequest(
      'api/ItemSourcesFiles/updateItemSourcesFiles',
      data,
      null
    );
  }
}
