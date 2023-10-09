import { PropertyType } from "../../Enumerations/Enums";


export interface SearchMultiProperty {
    PropertyName: string;
    PropertyValue: string | null;
    PropertyWithMultiValues: boolean;
    PropertyType: PropertyType;
    PropertyRangeValue1: Date | null;
    PropertyRangeValue2: Date | null;
}
export class MailRequest {
  toEmail: string;
  subject: string;
  body: string;
  id: string;
}


