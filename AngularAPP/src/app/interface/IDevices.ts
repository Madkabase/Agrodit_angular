export interface IDevices {
  FieldId: number;
Name: string;
DevEUI: string;
JoinEUI: string;
AppKey: string;
CalibrationMoisture1Max: number;
CalibrationMoisture1Min: number;
CalibrationMoisture2Max: number;
CalibrationMoisture2Min: number;
CalibrationSalinity1Max: number;
CalibrationSalinity1Min: number;
CalibrationSalinity2Max: number;
CalibrationSalinity2Min: number;
Location: number;
Id: number;
Status: string;
CompanyId: number;

  actions?: string;
}
