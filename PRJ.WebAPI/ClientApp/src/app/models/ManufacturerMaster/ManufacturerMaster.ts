import BaseModel from  "../Common/BaseModel" ;
import { Contries } from "../BaseContries/BasContries";

export interface ManufacturerMaster extends BaseModel {

    ManufacturerDescAr: string;
    ManufacturerDescEn: string;
    PhoneNo: string;
    MobileNo: string;
    EmailId: string;
    Location: string;
    AddressLine1: string;
    AddressLine2: string;
    AddressLine3: string;
    City: string;
    ZipCode: string;
    POBox: string;
    CountryId: number;
    BasCountries: Contries;




}