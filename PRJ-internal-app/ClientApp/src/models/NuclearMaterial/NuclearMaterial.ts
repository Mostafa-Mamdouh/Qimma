export interface NuclearMaterialRadionuclide {
  nuclearMaterialRadionulcideId?: string;
  enrichment: number;
  radionulcideId: string;
  materialId?: any;
  element: string;
}

export interface NuclearMaterialElements {
  id: string;
  elementMass: number;
  elementMassUnit: string;
  nuclearMaterialType: string;
  nuclearMaterialId: string;
  nuclearMaterialRadionulcides: NuclearMaterialRadionuclide[];
}

export interface NuclearMaterialFile {
  fileId: string;
  name: string;
  size: number;
  base64: string;
  contentType: string;
  attachmentType: string;
  nuclearMaterialId: string | null;
}

export interface NuclearMaterial {
  sourceId: string;
  nrrcId?: any;
  manufacturerSerialNo: string;
  facilitySerialNo: string;
  sourceModel: string;
  chemicalForm: string;
  isShield: boolean;
  status: string;
  physicalForm: string;
  quantity: number;
  quantityUnitId: string;
  entityId: string;
  facilityId: string;
  licenseId: string;
  licenseInventoryId?: any;
  permitdetailsId: string;
  permitInventoryId?: any;
  practiseId?: any;
  manufacturerId: string;
  manufacturerCountryId?: any;
  nuclearMaterialElements: NuclearMaterialElements[];
  nuclearMaterialFiles: NuclearMaterialFile[];
  id?: any;
  createdBy?: any;
  modifiedBy?: any;
  createdOn?: any;
  modifiedOn?: any;
  noManufacturerCertificateFlag: boolean | null;
  noCustomImportPermitFlag: boolean | null;
  noShipperImportPermitFlag: boolean | null;
  noImagesFlag: boolean | null;
  noCharacterizationCertificateFlag: boolean | null;
  noSourceTagImageFlag: boolean | null;
}
