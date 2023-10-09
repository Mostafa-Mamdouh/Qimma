import { Injectable } from '@angular/core';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { RadiationGeneratorsProfile } from 'src/app/models/RadiationGeneratorsProfile/RadiationGeneratorsProfile';

@Injectable({
  providedIn: 'root',
})
export class RadiationGeneratorsService {
  constructor(private helper: Helper) {}

  getAll() {
    return this.helper.GetRequest(
      'api/RadiationGeneratorsProfile/GetAll',
      null
    );
  }

  getRadiationGeneratorById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest(
      'api/RadiationGeneratorsProfile/GetRadiationGeneratorProfileById',
      params
    );
  }

  addRadiationGenerator(data: RadiationGeneratorsProfile) {
    return this.helper.AddRequest(
      'api/RadiationGeneratorsProfile/AddRadiationGeneratorProfile',
      data,
      null
    );
  }

  updateRadiationGenerator(data: RadiationGeneratorsProfile) {
    return this.helper.PutRequest(
      'api/RadiationGeneratorsProfile/UpdateRadiationGeneratorProfile',
      data,
      null
    );
  }
}
