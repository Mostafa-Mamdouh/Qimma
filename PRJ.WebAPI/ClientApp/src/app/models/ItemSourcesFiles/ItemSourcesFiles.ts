import BaseModel from  "../Common/BaseModel" ;
import { EntityProfile } from "../EntityProfile/EntityProfile";

export interface ItemSourcesFiles extends BaseModel {

    FileSourceID: number;
    FileNum: number;
    FileName: string;
    FileOriginalName: string;
    FileSize: number;
    UploadType: number;
    FileBytes: string;
    ContentType: string;

}