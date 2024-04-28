export interface IAlerts {
  Id: number;
Date: Date;
AlertType: number;
FieldId: number;
Closed: boolean;

  actions?: string;
}
