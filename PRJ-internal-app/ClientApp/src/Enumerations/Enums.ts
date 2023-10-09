export enum PropertyType {
  Normal = 1,
  DateRangeAndEqual = 2,
}

export enum FieldType {
  Date = 1,
  ListItem = 2,
  Text = 3,
  TextArea = 4,
  RadioButton = 5,
  CheckBox = 6,
  Button = 7,
  FileUpload = 8,
}

export enum TimeUnits {
  Seconds = 1,
  Minutes = 2,
  Days = 3,
  Years = 4,
}
export enum SourceTypes {
  sealed = 104,
  unsealed = 105,
  variableUnsealed = 106,
}
export enum DropDownType {
  Entity = 1,
  Facility = 2,
  License = 3,
  Permit = 4,
  Practise = 5,
}

export enum AttachmentsTypes {
  manufacturerCertificate = '1',
  customImportPermits = '2',
  customExportPermits = '3',
  shipperImportPermits = '4',
  shipperExportPermits = '5',
  otherDocumnets = '6',
  images = '7',
  charCertificates = '8',
  sourceTagImage = '9',
  endUserCertificate = '10',
}
