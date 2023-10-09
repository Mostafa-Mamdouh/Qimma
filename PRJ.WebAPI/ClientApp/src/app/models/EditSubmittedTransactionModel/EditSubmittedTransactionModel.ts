export interface EditSubmittedTransactionModel {
  id: string | null;
  createdBy: string | null;
  modifiedBy: string | null;
  createdOn: string | null;
  modifiedOn: string | null;
  itemSourceProfileId: string;
  transactionDate: string;
  transactionTypeId: number;
  transactionAttribute: number;
  oldValue: string;
  newValue: string;
  remarks: string;
}
