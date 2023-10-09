export class ItemSourceStatus {
  id: string;
  statusCode: string;
  statusNameEn: string;
  statusNameAr: string;
}

export class TransactionTypeMaster {
  id: string;
  transactionTypeDesFrn: string;
  transactionTypeDesNtv: string;
}
export class ActionCenter {
  sourceId: string;
  nrrcId: string;
  transactionTypeEn: string;
  transactionTypeAr: string;
  transactionDate: string | null;
  entityEn: string;
  entityAr: string;
  facilityEn: string;
  facilityAr: string;
  licenseEn: string;
  licenseAr: string;
  permitNumber: string;
  sourceTypeEn: string;
  sourceTypeAr: string;
  sourceCode: string;
  transactionStatusEn: string;
  transactionStatusAr: string;
  transactionId: number;
  createUser: string;
  modifiedUser: string;
  confirmedUser: string;
  createDate: string | null;
  modifiedDate: string | null;
  confirmedDate: string | null;
  manufacturerEn: string;
  manufacturerAr: string;
  manufacturerSerialNo: string;
  manufacturerCountryEn: string;
  manufacturerCountryAr: string;
  productionDate: string | null;
  facilitySourceID: string;
  radionuclideName: string;
  currentActivity: number;
  associatedEquipmentAr: string;
  associatedEquipmentEn: string;
  currentSourceStatusAr: string;
  currentSourceStatusEn: string;
  shieldCode: string;
  sourceType: number;
  quantity: number;
  itemSourceMsgHistories: ItemSourceMsgHistory[];
  itemSourceStatusHistories: ItemSourceStatusHistory[];
}
export class ItemSourceStatusHistory {
  remarks: string;
  fromStateEn: string;
  toStateEn: string;
  fromStateAr: string;
  toStateAr: string;
  entryUser: string;
  createDate: string;
}

export class ItemSourceMsgHistory {
  msgText: string;
  sentBy: string;
  createdOn: string;
}

export class ItemSourceStatusHistoryEditor {
  statusHistoryId: string;
  remarks: string;
  sourceId: string;
  statusId: string;
  parentStatusId: string;
}
