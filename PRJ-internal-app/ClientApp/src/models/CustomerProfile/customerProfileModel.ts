import BaseModel from  "../Common/BaseModel" ;

export interface DTOCustomerProfile {


    CustomerId: number;
        
    CustomerNameAr: string;

    CustomerNameEn: string;

    RefCode?: string;
  
    ActiveFlag?: boolean;

}
