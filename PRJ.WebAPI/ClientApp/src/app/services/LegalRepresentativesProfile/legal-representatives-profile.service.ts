import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { LegalRepresentativesProfile } from 'src/app/models/LegalRepresentativesProfile/LegalRepresentativesProfile';

@Injectable({
  providedIn: 'root',
})
export class LegalRepresentativesProfileService {
  constructor(private helper: Helper) {}

  getAll() {
    return this.helper.GetRequest(
      'api/LegalRepresentativesProfile/GetAll',
      null
    );
  }

  getLegalRepresentativesProfileById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest(
      'api/LegalRepresentativesProfile/GetLegalRepresentativesProfileById',
      params
    );
  }

  addLegalRepresentativesProfile(data: LegalRepresentativesProfile) {
    return this.helper.AddRequest(
      'api/LegalRepresentativesProfile/AddLegalRepresentativesProfile',
      data,
      null
    );
  }

  updateLegalRepresentativesProfile(data: LegalRepresentativesProfile) {
    return this.helper.PutRequest(
      'api/LegalRepresentativesProfile/updateLegalRepresentativesProfile',
      data,
      null
    );
  }
}
