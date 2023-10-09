import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';

@Injectable({
  providedIn: 'root',
})
export class NuclearMaterialServices {
  constructor(private helper: Helper) {}
  getRadionulicdesByNuclearMaterial(nm: string) {
    let parameter = new ParameterClass('nuclearmaterial', nm);
    return this.helper.GetRequest('api/Radionuclide/GetByNuclearMaterial', [
      parameter,
    ]);
  }

  getSourceById(id: string) {
    let parameter = new ParameterClass('Id', id);
    return this.helper.GetRequest('api/ItemSources/GetById', null, [parameter]);
  }

  addNuclearMaterial(body: any) {
    return this.helper.AddRequest(
      'api/NuclearMaterial/AddNuclearMaterial',
      body
    );
  }

  updateNuclearMaterial(body: any) {
    return this.helper.AddRequest(
      'api/NuclearMaterial/UpdateNuclearMaterial',
      body
    );
  }

  deleteNuclearMaterial(id: string) {
    let parameter = new ParameterClass('id', id);

    return this.helper.GetRequest(
      'api/NuclearMaterial/DeleteNuclearMaterial',
      null,
      [parameter]
    );
  }

  getNuclearMaterialById(id: string) {
    let parameter = new ParameterClass('id', id);
    return this.helper.GetRequest(
      'api/NuclearMaterial/GetNuclearMaterialById',
      null,
      [parameter]
    );
  }

  getNuclearMaterials() {
    return this.helper.GetRequest(
      'api/NuclearMaterial/GetNuclearMaterials',
      null
    );
  }

  getNuclearMaterialsFunctional(body: any) {
    return this.helper.GetRequest(
      'api/NuclearMaterial/GetNuclearMaterialsFunctional',
      body,
      null
    );
  }

  addNuclearShield(body: any) {
    return this.helper.AddRequest('api/NuclearMaterial/AddNuclearShield', body);
  }

  getNuclearMaterialsAsLookup(id: string) {
    let parameter = new ParameterClass('id', id);
    // let license = new ParameterClass('licenseId', licenseId);
    return this.helper.GetRequest(
      'api/NuclearMaterial/GetNuclearMaterialsAsLookUp',
      null,
      [parameter]
    );
  }

  getNuclearMaterialsForTransaction(id: string, sourceId: string) {
    let parameter = new ParameterClass('id', id);
    let parameter1 = new ParameterClass('sourceId', sourceId);
    return this.helper.GetRequest(
      'api/NuclearMaterial/GetNuclearMaterialsForTransaction',
      null,
      [parameter, parameter1]
    );
  }
}
