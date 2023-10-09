import BaseModel from '../Common/BaseModel';
import { EntityProfile } from '../EntityProfile/EntityProfile';

export interface AttachmentModel extends BaseModel {
  FileSourceID: string;
  FileName: string;
  FileSize: number;
  FileStringBase62: string;
  ContentType: string;
}
