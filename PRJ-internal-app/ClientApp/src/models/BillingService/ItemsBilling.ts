
export interface ItemsBilling
{
  lineNum: number | null;
    serviceItemId: string;
    itemQty: number | null;
    itemPrice: number| null;
    lineRemarks: string;
    vatIncluded: boolean;
    vatPcntg: number | null;
    vatAmount: number | null;
    billableQty: number | null;
    TotalpriceItem: number | null;
    Unit : string;
    itmQtyFromService :number | null;
}
export interface ItemsBillingHeader
{
  invoiceCode : string,
    invoiceDate : Date,
    transactionRefNum : string,
    statusFlag : string,
    trnRemarks : string,
    customerId : string,
    customerName : string,
    currencyCode : string,
    exRate : string,
    termsCode : string,
    invoiceSource : string,
    invoiceBU : string,
    itemHierarchyStructure :string
}





