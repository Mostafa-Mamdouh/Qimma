import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { SafetyResponsibleOfficersProfile } from 'src/app/models/SafetyResponsibleOfficersProfile/SafetyResponsibleOfficersProfile';

@Injectable({
  providedIn: 'root',
})
export class SafetyResponsibleOfficerService {
  constructor(private helper: Helper) {}

  getAll() {
    return this.helper.GetRequest(
      'api/SafetyResponsibleOfficersProfile/GetAll',
      null
    );
  }

  getRadiationGeneratorById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest(
      'api/SafetyResponsibleOfficersProfile/GetSafetyResponsibleOfficersProfileById',
      params
    );
  }

  addRadiationGenerator(data: SafetyResponsibleOfficersProfile) {
    return this.helper.AddRequest(
      'api/SafetyResponsibleOfficersProfile/AddSafetyResponsibleOfficersProfile',
      data,
      null
    );
  }

  updateRadiationGenerator(data: SafetyResponsibleOfficersProfile) {
    return this.helper.PutRequest(
      'api/SafetyResponsibleOfficersProfile/UpdateSafetyResponsibleOfficersProfile',
      data,
      null
    );
  }
}
