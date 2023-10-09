export enum TransactionTypes {
  NewSourceFromImport = 1,
  NewSourceFromTransfer = 2,
  ChangeFacilityId = 3,
  ChangeAssociatedEquipment = 4,
  ChangeSourceShield = 5,
  ChangeSourcePrimaryData = 6,
  ChangeSourceStatus = 7,
  ChangeSourceQuantity = 1001,
}

export enum TransactionAttributes {
  status = 1,
  facilitySourceId = 2,
  associatedEquipment = 3,
  shield = 4,
  quantity = 5,
}
