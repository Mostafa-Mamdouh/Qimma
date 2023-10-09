import BaseModel from '../Common/BaseModel';

export interface DTORelatedItems {
  RelatedItemCode: string;
  ManufacturerSerialNom: string;
  IsTechnologyAndSoftware: boolean;
  Description: string;
  EntityId: string;
  FacilityId: string;
  LicenseId: string;
  PermitdetailsId: string;
  RelatedItemFiles: any[];
  StatusID: string;
  ManufacturerId: string;
  ManufacturerCountryId: string;
  HierarchyStructureCode: string;
  noManufacturerCertificateFlag: boolean | null;
  noCustomImportPermitFlag: boolean | null;
  noShipperImportPermitFlag: boolean | null;
  noCharacterizationCertificateFlag: boolean | null;
  noSourceTagImageFlag: boolean | null;
}

export interface RelatedItemFiles {
  fileId: string;
  name: string;
  size: number;
  base64: string;
  contentType: string;
  attachmentType: string;
  relatedItemCode: string;
}
