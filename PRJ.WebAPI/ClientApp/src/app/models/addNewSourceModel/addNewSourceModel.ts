export interface AddNewSourceModel {
  id: string | null;
  transactionType: number;
  transactionHeaderId: string | null;
  entity: string | null;
  facility: string | null;
  licenseNumber: string | null;
  permitNumber: string | null;
  sourceType: string | null;
  manufacturerSerialNo: string | null;
  manufacturer: string | null;
  manufacturerCountry: string | null;
  status: string | null;
  facilitySourceID: string | null;
  associatedEquipment: string | null;
  radionuclides: any[];
  sourceAttachments: any[] | null;
  shieldNuclearMaterialCode: string | null;
  initialMass: number | null;
  initialMassUnit: string | null;
  shieldAttachments: any[] | null;
  physicalForm: string;
  isShieldDU: boolean | null;
  sourceActivity: number | null;
  sourceActivityUnit: string | null;
  sourceStatus: number | null;
  quantity: number | null;
  sourceModel: string | null;
  noManufacturerCertificateFlag: boolean | null;
  noCustomImportPermitFlag: boolean | null;
  noShipperImportPermitFlag: boolean | null;
  noImagesFlag: boolean | null;
  noCharacterizationCertificateFlag: boolean | null;
  noSourceTagImageFlag: boolean | null;
}

export interface Radionuclide {
  radionuclide: string;
  initialActivity: number;
  initialActivityUnit: string;
}
