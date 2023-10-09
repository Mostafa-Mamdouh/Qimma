import { Radionuclide } from '../addNewSourceModel/addNewSourceModel';

export interface AddNewNuclearMaterial {
  transactionType: number;
  entity: string;
  facility: string;
  licenseNumber: string;
  permitNumber: string;
  sourceType: string;
  manufacturerSerialNo: string;
  manufacturer: string;
  productionDate: Date;
  isotops: Radionuclide[];
  status: string;
  facilitySourceID: string;
  sourceAttachments: any[];
  weightOfNuclearMaterial: string;
  weightOfNuclearMaterialUnit: string;
  weightOfFissileIsotop: string;
  weightOfFissileIsotopUnit: string;
  continerSize: string;
  chemicalComposition: string;
  enrichmentOfUranium: string;
  physicalForm: string;
  chemicalForm: string;
  purpose: string;
  initialActivity: number;
  activityUnit: string;
  nuclearMaterial: string;
}
